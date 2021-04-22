using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises.DataLayer.DbModels
{
    public class Product
    {
        [Key]
        public string name { get; set; }
        public double price { get; set; }
    }
}
