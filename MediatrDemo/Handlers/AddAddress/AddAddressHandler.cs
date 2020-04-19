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
    }

    public class NotoficationHandler1 : INotificationHandler<AddressAddedEvent>
    {
        public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
        {
            // send an email
            return Task.CompletedTask;
        }
    }
    
    public class NotoficationHandler2 : INotificationHandler<AddressAddedEvent>
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
            // save to the DB
            await _mediator.Publish(new AddressAddedEvent(), cancellationToken);
            return new AddAddressResponse();
        }
    }
}