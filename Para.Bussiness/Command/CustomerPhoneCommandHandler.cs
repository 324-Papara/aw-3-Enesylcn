using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;
public class CustomerPhoneCommandHandler :

    IRequestHandler<CreateCustomerPhoneCommand, ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<UpdateCustomerPhoneCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerPhoneCommand, ApiResponse>
{
    private readonly IUnitOfWork<Para.Data.Domain.CustomerPhone> unitOfWork;
    private readonly IMapper mapper;

    public CustomerPhoneCommandHandler(IUnitOfWork<Para.Data.Domain.CustomerPhone> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerPhoneResponse>> Handle(CreateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerPhoneRequest, CustomerPhone>(request.Request);
        mapped.CustomerId = request.CustomerId;
        await unitOfWork.GenericRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerPhoneResponse>(mapped);
        return new ApiResponse<CustomerPhoneResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        var customerPhone = await unitOfWork.GenericRepository.GetById(request.Id);
		var mapped = mapper.Map(request.Request, customerPhone);
	    unitOfWork.GenericRepository.Update(mapped);
		await unitOfWork.Complete();
		return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerPhoneCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.GenericRepository.Delete(request.Id);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}