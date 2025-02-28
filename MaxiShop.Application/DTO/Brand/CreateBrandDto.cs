﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Brand
{
    public class CreateBrandDto
    {
       
        public string Name { get; set; }
        public int EstablishedYear { get; set; }
    }
    public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x=>x.EstablishedYear).InclusiveBetween(1999, 2002).NotEmpty().NotNull(); 
        }
    }
}
