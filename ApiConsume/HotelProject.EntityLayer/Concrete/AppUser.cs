﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? City { get; set; }
        public string? ImageURL { get; set; }
        public string? AccessFailedCount { get; set; }
        public string? LockoutEnabled { get; set; }
        public string? PhoneNumberConfirmed { get; set; }
        public string? EmailConfirmed { get; set; }
    }
}
