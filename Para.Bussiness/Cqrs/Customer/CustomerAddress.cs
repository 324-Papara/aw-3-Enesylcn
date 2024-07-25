using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerAddressCommand(long CustomerId,CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAddressCommand(long Id,CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAddressCommand(long Id) : IRequest<ApiResponse>;

public record GetAllCustomerAddressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetCustomerAddressByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;