using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAdressesController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerAdressesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute]long customerId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> Post(long customerId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAddressCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{Id}")]
        public async Task<ApiResponse> Put(long Id, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAddressCommand(Id,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> Delete(long Id)
        {
            var operation = new DeleteCustomerAddressCommand(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}