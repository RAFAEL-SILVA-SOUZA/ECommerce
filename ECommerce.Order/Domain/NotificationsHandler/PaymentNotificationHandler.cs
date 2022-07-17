using DotNetCore.CAP;
using ECommerce.Order.Domain.Notifications;
using Flurl;
using Flurl.Http;
using MediatR;

namespace ECommerce.Order.Domain.NotificationsHandler
{
    public class PaymentNotificationHandler : INotificationHandler<PaymentNotification>
    {
        private readonly Url _baseUrl;
        public PaymentNotificationHandler(IConfiguration configuration)
        {
            _baseUrl = configuration["Payment:url"];
        }

        public Task Handle(PaymentNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var request = _baseUrl.AppendPathSegment("/");
                request.PostJsonAsync(notification);
            });
        }
    }
}
