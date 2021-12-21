﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avander.VehicleApp.Domain.Entities
{
    public class Shop
    {
        public int ShopId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
    }
}