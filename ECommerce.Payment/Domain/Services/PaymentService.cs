using DotNetCore.CAP;
using ECommerce.Payment.Domain.Entities.Enums;
using ECommerce.Payment.Infra;
using ECommerce.Payment.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Data.SqlClient;

namespace ECommerce.Payment.Domain.Services
{
    public class PaymentService : ICapSubscribe, IPaymentService
    {
        private readonly PaymentDbContext _paymentDbContext;
        private readonly IConfiguration _configuration;
        private readonly ICapPublisher _capPublisher;
        private readonly Url _baseUrl;
        public PaymentService(PaymentDbContext paymentDbContext,
            IConfiguration configuration,
            ICapPublisher capPublisher)
        {
            _paymentDbContext = paymentDbContext;
            _configuration = configuration;
            _capPublisher = capPublisher;
            _baseUrl = configuration["Gateway:url"];
        }

        [CapSubscribe("ecomerce.payment.proccess")]
        public async Task ProccessPayment(PaymentRequest paymentEntity)
        {
            var paymentResponseDto = await ProccessPaymentCielo(paymentEntity);
            paymentResponseDto.OrderId = paymentEntity.OrderId;

            await using var connection = new SqlConnection(_configuration["ConnectionStrings:PaymentConnection"]);
            using var transaction = connection.BeginTransaction(_capPublisher, autoCommit: true);
            await _capPublisher.PublishAsync("ecomerce.order.status.payment", paymentResponseDto);

            await _paymentDbContext.Payments.AddAsync(new Entities.Payment
            {
                Amount = paymentEntity.Amount,
                GatewayName = paymentResponseDto.GatewayName,
                OrderId = paymentResponseDto.OrderId,
                PaymentStatus = paymentResponseDto.PaymentStatus,
                ProccessDate = paymentResponseDto.ProccessDate,
                TranzactionId = paymentResponseDto.TranzactionId
            });
            await _paymentDbContext.SaveChangesAsync();
        }

        private async Task<PaymentResponse> ProccessPaymentCielo(PaymentRequest paymentEntity)
        {
            var request = _baseUrl.AppendPathSegment("/cielo");
            var response = await request.PostJsonAsync(paymentEntity);
            var paymentResponseDto = await response.GetJsonAsync<PaymentResponse>();

            if (paymentResponseDto.PaymentStatus == PaymentStatus.Rejected)
                return await ProccessPaymentStone(paymentEntity);

            paymentResponseDto.GatewayName = "Cielo";
            return paymentResponseDto;
        }

        private async Task<PaymentResponse> ProccessPaymentStone(PaymentRequest paymentEntity)
        {
            _baseUrl.RemovePathSegment();
            var request = _baseUrl.AppendPathSegment("/stone");
            var response = await request.PostJsonAsync(paymentEntity);
            var paymentResponseDto = await response.GetJsonAsync<PaymentResponse>();

            if (paymentResponseDto.PaymentStatus == PaymentStatus.Accepted)
                paymentResponseDto.GatewayName = "Stone";

            return paymentResponseDto;
        }
    }
}
