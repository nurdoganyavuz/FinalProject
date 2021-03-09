using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection); //bağımlılıkları -> injectionları yükleyecek.
    }
}

//bütün projelerde ortak olan service'lerin mesela -> HttpContextAccessor gibi
//injection edilmesi için core katmanında olusturduk.
//autofacbusinessmodule'da sadece northwind projemizde kullanacagımız servislerin injection'ını yapmıştık.
//ancak autofac'in genel bağımlılıkları da bilmesi lazım ki ona göre işlem yapabilsin.
//mesela IHttpContextAccessor çağırıldıgında arkaplanda hangi class'a karşılık geldiğini bilmesi gerek.
//bunun için bütün projelerde ortak olacak olan service'lerin injection işlemini core katmanımızda tutarız.
//biz burada bir service vereceğiz ve yükleme(Load) işlemini bu class sayesinde gerçekleştiricez.

