﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MotoWebShop.Models
{
    public class OrderHead
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
    }
}
