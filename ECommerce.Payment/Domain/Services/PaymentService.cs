using DotNetCore.CAP;
using ECommerce.Payment.Domain.Entity;
using ECommerce.Payment.Dtos;
using Flurl;
using Flurl.Http;
using Microsoft.Data.SqlClient;

namespace ECommerce.Payment.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ICapPublisher _capPublisher;
        private readonly Url _baseUrl;
        public PaymentService(IConfiguration configuration,
            ICapPublisher capPublisher)
        {
            _configuration = configuration;
            _capPublisher = capPublisher;
            _baseUrl = configuration["Cielo:url"];
        }

        public async Task ProccessPayment(PaymentEntity paymentEntity)
        {
            var paymentResponseDto = await ProccessPaymentCielo(paymentEntity);
            paymentResponseDto.OrderId = paymentEntity.OrderId;

            await using var connection = new SqlConnection(_configuration["ConnectionStrings:PaymentConnection"]);
            using var transaction = connection.BeginTransaction(_capPublisher, autoCommit: true);
            await _capPublisher.PublishAsync("ecomerce.order.payment", paymentResponseDto);
        }

        private async Task<PaymentResponseDto> ProccessPaymentCielo(PaymentEntity paymentEntity)
        {
            var request = _baseUrl.AppendPathSegment("/cielo");
            var response = await request.PostJsonAsync(paymentEntity);
            var paymentResponseDto = await response.GetJsonAsync<PaymentResponseDto>();

            if (paymentResponseDto.PaymentStatus == PaymentStatus.Rejected)
                return await ProccessPaymentStone(paymentEntity);

            paymentResponseDto.GatewayName = "Cielo";
            return paymentResponseDto;
        }

        private async Task<PaymentResponseDto> ProccessPaymentStone(PaymentEntity paymentEntity)
        {
            _baseUrl.RemovePathSegment();
            var request = _baseUrl.AppendPathSegment("/stone");
            var response = await request.PostJsonAsync(paymentEntity);
            var paymentResponseDto = await response.GetJsonAsync<PaymentResponseDto>();

            if (paymentResponseDto.PaymentStatus == PaymentStatus.Accepted)
                paymentResponseDto.GatewayName = "Stone";

            return paymentResponseDto;
        }
    }
}
