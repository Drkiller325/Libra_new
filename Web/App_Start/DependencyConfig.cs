using Application.Extentions;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using FluentValidation;
using MediatR;
using Microsoft.Owin.Security;
using Persistence;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.App_Start
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });

       

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register(a => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.RegisterAssemblyTypes(typeof(ApplicationDependencyInjection).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IValidator<>))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            InfrastructureDependencyInjection.Register(builder);
            ApplicationDependencyInjection.RegisterApplication(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            
        }
    }
}