using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediatrDemo.Handlers.AddAddress
{
    public class AddAddressRequest : IRequest<AddAddressResponse>
    {
        public int UserId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
    }

    public class AddAddressResponse
    {
    }

    public class AddressAddedEvent : INotification
    {
        public int UserId { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string StreetAddress { get; }

        internal AddressAddedEvent(int userId, string city, string postalCode, string streetAddress)
        {
            UserId = userId;
            City = city;
            PostalCode = postalCode;
            StreetAddress = streetAddress;
        }
    }

    public class NotificationHandler1 : INotificationHandler<AddressAddedEvent>
    {
        public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
        {
            // send an email
            return Task.CompletedTask;
        }
    }
    
    public class NotificationHandler2 : INotificationHandler<AddressAddedEvent>
    {
        public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
        {
            // write a log
            return Task.CompletedTask;
        }
    }

    public class AddAddressHandler : IRequestHandler<AddAddressRequest, AddAddressResponse>
    {
        private readonly IMediator _mediator;

        public AddAddressHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<AddAddressResponse> Handle(AddAddressRequest request, CancellationToken cancellationToken)
        {
            // entry point -> from there send an event -> in NotoficationHandlers process that event
            // it's a good way of breaking up a complicated logic into multiple steps
            var addressAddedEvent = new AddressAddedEvent(request.UserId, request.City, request.PostalCode, request.StreetAddress);
            await _mediator.Publish(addressAddedEvent, cancellationToken);
            return new AddAddressResponse();
        }
    }
}