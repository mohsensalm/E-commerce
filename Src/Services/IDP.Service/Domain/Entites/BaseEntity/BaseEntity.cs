﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entites.BaseEntity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public Int64 ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
