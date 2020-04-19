using System.Threading.Tasks;
using MediatR;
using MediatrDemo.Handlers.AddAddress;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MediatrDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(
            ILogger<IndexModel> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            var addAddressRequest = new AddAddressRequest
            {
                UserId = 1,
                City = "Bratislava",
                PostalCode = "82103",
                StreetAddress = "Ruzinovska"
            };
            await _mediator.Send(addAddressRequest);
        }
    }
}