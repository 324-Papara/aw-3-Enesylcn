using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerPhoneQueryHandler : 
    IRequestHandler<GetAllCustomerPhoneQuery,ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetCustomerPhoneByIdQuery,ApiResponse<CustomerPhoneResponse>>

{
    private readonly IUnitOfWork<Para.Data.Domain.CustomerPhone> unitOfWork;
    private readonly IMapper mapper;

    public CustomerPhoneQueryHandler(IUnitOfWork<Para.Data.Domain.CustomerPhone> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken)
    {
        List<CustomerPhone> entityList = await unitOfWork.GenericRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);
        return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.GenericRepository.GetById(request.Id);
        var mapped = mapper.Map<CustomerPhoneResponse>(entity);
        return new ApiResponse<CustomerPhoneResponse>(mapped);
    }
}