using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService; //bu servise bağımlılığı olduğu için injection yaptık.

        public ProductsController(IProductService productService) //soyut sınıfa bağlı yani zayıf bağımlılık var -> loosely coupled
        {                                                        //Yani Iproductservice'e bağımlı ama soyut bir class oldugu için zayıf bağımlılık var. Dolayısıyla Manager'da değişiklik olursa burada bir sorun yaşamayacagız.
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data); //result'ın success'i true ise Ok durumunu döndürür. Ok -> 200 ok -> requestin basarılı oldugunu belirtir. 
            }                          //result.data -> succes true ise result'ın sadece datasını döndürür, success ya da message gelmez.
            return BadRequest(result.Message); //result'ın succes'i false ise BadRequest döndürür. result.message yaptıgımız için success false ise result'ın sadece mesajını döndürür.
           
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result); //yukarıdaki gibi result.data, result.message şeklinde de kullanmadıgımız için result'ın tüm degerlerini getirir -> success, message, data.
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success == true) //direkt result.Success yazmakla aynı sey.
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

//NOTLAR

//IProductService _productService; 

//public ProductsController(IProductService productService){
//    _productService = productService;
//}

//Burada ctor içine IProductService verdik, yani controller çalışmaya basladıgında IProductService'i implement eden bir class istiyoruz. 
//program bunun productmanager oldugunu çözümleyemez. çünkü birden fazla class IProductService'i implement edebilir. hangisi oldugunu bilemez.
//burada direkt productmanager verseydik, yani somut class. ona bagımlı olmus olurduk. BUNUN YERİNE;
//IoC Container -- Inversion of control yapısını kullanırız. ctor'daki soyut sınıf parametresinin arkaplanda hangi class'ın referansını tutuyorsa
//o class'ın newlenmiş hali IoC'de tutulur. ProductsController çalıştıgında IoC kutusu kontrol edilir ve 
//ctor'daki Iproductservice hangi classın referansını tutuyorsa o getirilir. böylece controller ıproductservice'i çözümlemiş olur.


//STARTUP NOT
//IoC yapısı

//services.AddSingleton<IProductService, ProductManager>(); *1*

//biri ctor'da IProductService isterse; ona (IoC'de tutulan) newlenmiş ProductManager verilir.
//yani AddSingleton; programın herhangi bir yerinde Iproductservice istenildiğinde, arkaplanda ProductManager'ı newleyip istenilen yere gönderiyor.

//services.AddSingleton<IProductDal, EfProductDal>(); *2*

//eğer birisi senden IProductDal isterse, arkapalanda onun için bir EfProductDal newle ve gönder diyoruz programa.

