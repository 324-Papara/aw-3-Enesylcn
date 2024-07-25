using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Para.Data.CustomerDapperRepository;
using Para.Data.Domain;
using Para.Data.UnitOfWork;

namespace Para.Bussiness.RegistrationServices
{
    public class AutofacBusinessModule: Module
    {
        // Tüm servislerin register edildimesi.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork<Customer>>().As<IUnitOfWork<Customer>>().SingleInstance();
            builder.RegisterType<UnitOfWork<CustomerDetail>>().As<IUnitOfWork<CustomerDetail>>().SingleInstance();
            builder.RegisterType<UnitOfWork<CustomerAddress>>().As<IUnitOfWork<CustomerAddress>>().SingleInstance();
            builder.RegisterType<UnitOfWork<CustomerPhone>>().As<IUnitOfWork<CustomerPhone>>().SingleInstance();
            builder.RegisterType<CustomerDapperRepository>().As<ICustomerDapperRepository>().SingleInstance();

        }
    }

}