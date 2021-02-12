using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; 

        public ProductManager(IProductDal productDal) 
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length<2)
            {
                return new ErrorResult(Messages.ProductNameInvalid); //girilen ürün ismi 2 karakterden az ise ProductNameInvalid mesajını döndürecek, ve eklemeyecek.
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded); //girilen ürün eklendiğinde ProductAdded mesajını döndürecek.
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22) //sistemin anlık saati 22 ise; bu operasyon çalışmasın ve sistem bakımda mesajı versin.
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed); 
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id)); //programda gönderilen kategori id'yi filtre olarak alır ve her ürünün kategori id'sine bakar, 
                                                                                                     //gönderilen kategori id ile kategori id'si EŞİT olan tüm ürünleri getirir.
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 03) //sistemin anlık saati 03 ise; bu operasyon çalışmasın ve sistem bakımda mesajı versin.
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}

