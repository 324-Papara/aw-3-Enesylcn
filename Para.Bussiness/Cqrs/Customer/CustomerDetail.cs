using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerDetailCommand(long CustomerId,CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record UpdateCustomerDetailCommand(long Id,CustomerDetailRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerDetailCommand(long Id) : IRequest<ApiResponse>;

public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
public record GetCustomerDetailByIdQuery(long Id) : IRequest<ApiResponse<CustomerDetailResponse>>;
