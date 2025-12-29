using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;
using Autofac.Integration.Mvc;
using System.Reflection;
using Application.Extentions;
using System.Web.UI;
using FluentValidation;

namespace Web.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(ApplicationDependencyInjection).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IValidator<>))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            
        }
    }
}