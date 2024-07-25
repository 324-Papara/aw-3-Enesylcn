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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        // GET api/<CustomersController>/1
        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerResponse>> Get([FromRoute]long customerId)
        {
            var operation = new GetCustomerByIdQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest value)
        {
            var operation = new CreateCustomerCommand(value);
            var result = await mediator.Send(operation);
            return result;
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
        {
            var operation = new UpdateCustomerCommand(customerId,value);
            var result = await mediator.Send(operation);
            return result;
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerCommand(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}/where")]
        public async Task<ApiResponse<List<CustomerResponse>>> GetWhere([FromRoute]long customerId)
        {
            var operation = new GetCustomerByIdWhereQuery(customerId);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("Include/CustomerPhones")]
        public async Task<ApiResponse<List<CustomerResponse>>> GetCustomer()
        {
            var operation = new GetCustomerByIdIcludeQuery();
            var result = await mediator.Send(operation);
            return result;
        }
    }
}