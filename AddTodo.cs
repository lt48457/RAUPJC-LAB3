﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AddTodo
    {
	 [Required, MaxLength(60)]
        public string Text { get; set; }
    }
}
