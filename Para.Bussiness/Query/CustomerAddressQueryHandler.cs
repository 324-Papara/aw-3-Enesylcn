using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerAddressQueryHandler : 
    IRequestHandler<GetAllCustomerAddressQuery,ApiResponse<List<CustomerAddressResponse>>>,
    IRequestHandler<GetCustomerAddressByIdQuery,ApiResponse<CustomerAddressResponse>>

{
    private readonly IUnitOfWork<Para.Data.Domain.CustomerAddress> unitOfWork;
    private readonly IMapper mapper;

    public CustomerAddressQueryHandler(IUnitOfWork<Para.Data.Domain.CustomerAddress> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAddressQuery request, CancellationToken cancellationToken)
    {
        List<CustomerAddress> entityList = await unitOfWork.GenericRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
        return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.GenericRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerAddressResponse>(entity);
        return new ApiResponse<CustomerAddressResponse>(mapped);
    }
}