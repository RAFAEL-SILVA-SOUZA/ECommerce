using ECommerce.Order.Domain.Entities;
using ECommerce.Order.Domain.Entities.Enums;
using ECommerce.Order.Domain.Notifications;
using ECommerce.Order.Dtos;
using ECommerce.Order.Infra;
using Flurl.Util;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Order.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly ICatalogItemService _catalogItemService;
        private readonly IMediator _mediator;

        public OrderService(OrderDbContext orderDbContext, ICatalogItemService catalogItemService, IMediator mediator)
        {
            _orderDbContext = orderDbContext;
            _catalogItemService = catalogItemService;
            _mediator = mediator;
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

            foreach (var item in items)
            {
                var quantity = orderCreateDto.Itens.Single(x => x.ItemId == item.Id);
                order.AddItens(new OrderItem(item.Description, item.Price, quantity.Quantity));
            }

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();

            await _mediator.Publish(new PaymentNotification(orderCreateDto.CardName,
                orderCreateDto.CardNumber,
                orderCreateDto.ValidDate,
                orderCreateDto.Cvv,
                order.Id,
                order.TotalAmount));

            return await GetOrderById(order.Id);
        }

        public async Task<OrderEnityDto> ReproccessOrder(OrderReproccessDto orderReproccessDto)
        {
            var order = await _orderDbContext.Orders
                .Include(x=>x.Itens)
                .SingleAsync(x=>x.Id == orderReproccessDto.Id);

            if (order.OrderStatus == OrderStatus.Acepted)
            {
                throw new ArgumentException("Ordem com pagamento efetuado, não pode ser reprocessada");
            }

            order.Itens.Clear();
            order.TotalAmount = 0;

            var items = await _catalogItemService
                .GetProducts(orderReproccessDto.Itens.Select(x => x.ItemId)
                    .ToArray());

            foreach (var item in items)
            {
                var quantity = orderReproccessDto.Itens.Single(x => x.ItemId == item.Id);
                order.AddItens(new OrderItem(item.Description, item.Price, quantity.Quantity));
            }

            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync();
            await _mediator.Publish(new PaymentNotification(orderReproccessDto.CardName,
                orderReproccessDto.CardNumber,
                orderReproccessDto.ValidDate,
                orderReproccessDto.Cvv,
                order.Id,
                order.TotalAmount));

            return await GetOrderById(order.Id);
        }

        public async Task ChangeStatus(Guid id, OrderStatus orderStatus, string gatewayName, Guid tranzactionId)
        {
            var order = await _orderDbContext.Orders.FindAsync(id);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
                order.GatewayName = gatewayName;
                order.TranzactionId = tranzactionId;
            }

            _orderDbContext.Orders.Update(order);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
