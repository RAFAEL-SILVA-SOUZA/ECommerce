using DotNetCore.CAP;
using ECommerce.Order.Domain.Entities;
using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Domain.Messages;
using ECommerce.Order.Dtos;
using ECommerce.Order.Infra;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Order.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ICatalogItemService _catalogItemService;
        private readonly ICapPublisher _capPublisher;

        public OrderService(OrderDbContext orderDbContext, ICatalogItemService catalogItemService, ICapPublisher capPublisher)
        {
            _orderDbContext = orderDbContext;
            _catalogItemService = catalogItemService;
            _capPublisher = capPublisher;
        }

        public async Task<IList<OrderEnityDto>> GetAllOrders()
        {
            var orders = await _orderDbContext.Orders
                .Include(X => X.Itens)
                .AsNoTracking()
                .ToListAsync();
            return orders.Select(x => new OrderEnityDto(x)).ToList();
        }

        public async Task<OrderEnityDto> GetOrderById(Guid id)
        {
            var order = await _orderDbContext.Orders
                .Include(X => X.Itens)
                .FirstOrDefaultAsync(X => X.Id == id);
            return order == null ? default : new OrderEnityDto(order);
        }

        public async Task<OrderEnityDto> CreateOrder(OrderCreateDto orderCreateDto)
        {
            var order = new OrderEntity();
            var items = await _catalogItemService
                .GetProducts(orderCreateDto.Itens.Select(x => x.ItemId)
                    .ToArray());

            var verifyStock = from item in items
                              join itemOrder in orderCreateDto.Itens on item.Id equals itemOrder.ItemId
                              where item.Quantity < itemOrder.Quantity
                              select item;

            if (verifyStock.Any())
            {
                throw new ArgumentException("Item(s) do pedido sem estoque.");
            }

            foreach (var item in items)
            {
                var itemOrder = orderCreateDto.Itens.Single(x => x.ItemId == item.Id);
                order.AddItens(new OrderItem(item.Description, item.Price, itemOrder.Quantity, itemOrder.ItemId));
            }

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();

            await _capPublisher.PublishAsync("ecomerce.payment.proccess", new PaymentMessage(orderCreateDto.CardName,
                orderCreateDto.CardNumber,
                orderCreateDto.ValidDate,
                orderCreateDto.Cvv,
                order.Id,
                order.TotalAmount));

            return new OrderEnityDto(order);
        }

        public async Task<OrderEnityDto> ReproccessOrder(OrderReproccessDto orderReproccessDto)
        {
            var order = await _orderDbContext.Orders
                .Include(x => x.Itens)
                .SingleAsync(x => x.Id == orderReproccessDto.Id);

            if (order.OrderStatus == OrderStatus.Acepted)
            {
                throw new ArgumentException("Ordem com pagamento efetuado, não pode ser reprocessada");
            }

            var verifyStock = from item in order.Itens
                              join itemOrder in orderReproccessDto.Itens on item.Id equals itemOrder.ItemId
                              where item.Quantity < itemOrder.Quantity
                              select item;

            if (verifyStock.Any())
            {
                throw new ArgumentException("Item(s) do pedido sem estoque.");
            }

            order.Itens.Clear();
            order.TotalAmount = 0;
            order.OrderStatus = OrderStatus.Reprocessing;

            var items = await _catalogItemService
                .GetProducts(orderReproccessDto.Itens.Select(x => x.ItemId)
                    .ToArray());

            foreach (var item in items)
            {
                var itemOrder = orderReproccessDto.Itens.Single(x => x.ItemId == item.Id);
                order.AddItens(new OrderItem(item.Description, item.Price, itemOrder.Quantity, itemOrder.ItemId));
            }

            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync();

            await _capPublisher.PublishAsync("ecomerce.payment.proccess", new PaymentMessage(orderReproccessDto.CardName,
                orderReproccessDto.CardNumber,
                orderReproccessDto.ValidDate,
                orderReproccessDto.Cvv,
                order.Id,
                order.TotalAmount));

            return await GetOrderById(order.Id);
        }

        public async Task ChangeStatus(Guid id, OrderStatus orderStatus, string gatewayName, Guid tranzactionId)
        {
            var order = await _orderDbContext
                .Orders
                .Include(x => x.Itens)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order != null)
            {
                order.OrderStatus = orderStatus;
                order.GatewayName = gatewayName;
                order.TranzactionId = tranzactionId;
                order.UpdatedAt = DateTime.Now;
            }

            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync();

            if (order.OrderStatus == OrderStatus.Acepted)
                await _capPublisher.PublishAsync("ecomerce.catalog.stock", order.Itens.Select(x =>
                    new
                    {
                        ItemId = x.ProductId,
                        x.Quantity
                    }).ToArray());
        }
    }
}
