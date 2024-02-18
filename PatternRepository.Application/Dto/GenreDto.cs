﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRepository.Application.Dto
{
    public class GenreDto
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
