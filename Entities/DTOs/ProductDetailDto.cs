using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDetailDto : IDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public short UnitsInStock { get; set; }
    }
}

//generate type / generate new type / public / interface / IDto / Core / create new file / "Entities/IDto.cs" / OK.