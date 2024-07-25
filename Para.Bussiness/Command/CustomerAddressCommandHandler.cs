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
public class CustomerAddressCommandHandler :

    IRequestHandler<CreateCustomerAddressCommand, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<UpdateCustomerAddressCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerAddressCommand, ApiResponse>
{
    private readonly IUnitOfWork<Para.Data.Domain.CustomerAddress> unitOfWork;
    private readonly IMapper mapper;

    public CustomerAddressCommandHandler(IUnitOfWork<Para.Data.Domain.CustomerAddress> unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
       mapped.CustomerId = request.CustomerId;
        await unitOfWork.GenericRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerAddressResponse>(mapped);
        return new ApiResponse<CustomerAddressResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customerAddress = await unitOfWork.GenericRepository.GetById(request.Id);
		var mapped = mapper.Map(request.Request, customerAddress);
	    unitOfWork.GenericRepository.Update(mapped);
		await unitOfWork.Complete();
		return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.GenericRepository.Delete(request.Id);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}