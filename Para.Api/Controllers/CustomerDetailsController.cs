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
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomerDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{Id}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute]long Id)
        {
            var operation = new GetCustomerDetailByIdQuery(Id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> Post(long customerId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{Id}")]
        public async Task<ApiResponse> Put(long Id, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommand(Id,value);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerDetailCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}