using System;
using Autofac;
using Core.Interfaces;
using Core.Repositories;
using Core.Services;

namespace Core.AutoFac
{
    public class DepandencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterType<ShopService>().As<IShopService>().InstancePerLifetimeScope();
            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseDriver>().As<IDatabaseDriver>().InstancePerLifetimeScope();
        }
    }
}
