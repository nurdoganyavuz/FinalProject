using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
//**
//IServiceCollection'a AddDependencyResolvers operasyonunu ekleyerek extend edicez.
//bu operasyon sayesinde, startup'ta service'e istediğimiz modulleri ekleyebilcez.
//mesela ICoreModule'ü startupta servise eklememiz gerekiyosa bu operasyon sayesinde ekleyebiliriz.
//bu extension işlemi sayesinde bir ya da daha fazla DependencyResolver modülünü servise ekleyebilir hale gelicez.

//IServiceCollection -> asp.net uygulamamızın yani kısacası APImizin
//servis bağımlılıklarını eklediğimiz, ya da araya girmesini istediğimiz operasyonları içeren
//koleksiyonun ta kendisidir.

//**
//burada yaptıgımız işlem ile;
//projemizin core katmanı da dahil olmak üzere bütün katmanlarda 
//ekleyeceğimiz bütün injectionları bir arada tutabileceğimiz bir yapı olusturmus olduk.
//istediğimiz kadar modülü startup içinde ekleyebileceğiz

//operasyona ICoreModule vermemizin nedeni ise, ICoreModule'ün servicecollection yapısı ile olusturulması;
//ihtiyaç duydugumuz "evrensel" service injectionlarını coremodule de tutabiliyoruz.
//httpcontextaccessor'ü eklediğimiz gibi birden fazla bağımlılık injection'ı ekleyebiliriz.

//ICoreModule'ı implement eden bütün module class'ları bu operasyonda kullanabiliriz. (*****)

