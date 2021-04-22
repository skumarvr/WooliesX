using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises.DataLayer.DbModels
{
    [Keyless]
    public class ShopperHistory
    {
        public int customerId { get; set; }

        public string productName { get; set; }

        public int quantity { get; set; }

        public virtual Product product { get; set; }
    }
}
