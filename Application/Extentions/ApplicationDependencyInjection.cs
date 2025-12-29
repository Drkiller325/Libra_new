using Autofac;
using Autofac.Integration.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace Application.Extentions
{
    public class ApplicationDependencyInjection
    {

        public static void RegisterApplication(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ApplicationDependencyInjection).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IValidator<>))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterHubs(Assembly.GetExecutingAssembly());
        }
    }
}
