using System;
using System.Collections.Generic;

#nullable disable

namespace MVC_EntityFramework.Models
{
    //1. One time install only
    //dotnet tool install --global dotnet-ef

    //2.
    //dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    //dotnet add package Microsoft.EntityFrameworkCore.Design

    //3.
    //dotnet ef dbcontext scaffold "Server=.\sqlexpress;Database=MyPetShop;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
        public byte[] Picture { get; set; }

        public virtual Category CategoryNavigation { get; set; }
    }
}
