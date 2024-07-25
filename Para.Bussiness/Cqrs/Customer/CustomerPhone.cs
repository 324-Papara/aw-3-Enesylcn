using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerPhoneCommand(long CustomerId,CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long Id,CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long Id) : IRequest<ApiResponse>;

public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long Id) : IRequest<ApiResponse<CustomerPhoneResponse>>;