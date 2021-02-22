﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); //IProductService çağırıldıgı zaman kullanılması için *BİR TANE* ProductManager instance'ı olusturulur.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

//Bellekte ProductManager için bir instance olusturulur ve IProductService interface'inin çağırıldıgı her yere bu instance gönderilir.
//böylece her işlem için yeniden ProductManager instance'ı olusturmamıza(newlememize) gerek kalmaz. Bunu yapmamış olsaydık onlarca instance olusturmak zorunda kalırdık.
