using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CustomerQueryHandler : 
    IRequestHandler<GetAllCustomerQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdQuery,ApiResponse<CustomerResponse>>,
    IRequestHandler<GetCustomerByIdWhereQuery,ApiResponse<List<CustomerResponse>>>,
    IRequestHandler<GetCustomerByIdIcludeQuery,ApiResponse<List<CustomerResponse>>>

{
    private readonly IUnitOfWork<Para.Data.Domain.Customer> unitOfWork;
    private readonly IMapper mapper;

    public CustomerQueryHandler(IUnitOfWork<Para.Data.Domain.Customer> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> entityList = await unitOfWork.GenericRepository.GetAll();
        var mappedList = mapper.Map<List<CustomerResponse>>(entityList);
        return new ApiResponse<List<CustomerResponse>>(mappedList);
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.GenericRepository.GetById(request.CustomerId);
        var mapped = mapper.Map<CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }
    //Getting customer which one is equals ıd's in where statement 
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByIdWhereQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.GenericRepository.Where(x => x.Id == request.CustomerId);
        var mappedResponse = mapper.Map<List<CustomerResponse>>(entity);
        return new ApiResponse<List<CustomerResponse>>(mappedResponse);
      
    }
    //Getting customer list which one ınclude CustomerPhones 
    public async Task<ApiResponse<List<CustomerResponse>>> Handle(GetCustomerByIdIcludeQuery request, CancellationToken cancellationToken)
    {
        var entityList = await unitOfWork.GenericRepository.Include(x => x.CustomerPhones);
        var mappedResponse = mapper.Map<List<CustomerResponse>>(entityList);
        return new ApiResponse<List<CustomerResponse>>(mappedResponse);
    }
   
}