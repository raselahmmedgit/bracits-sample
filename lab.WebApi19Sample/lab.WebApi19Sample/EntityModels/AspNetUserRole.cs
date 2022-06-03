using System;
using System.Collections.Generic;

#nullable disable

namespace lab.WebApi19Sample.EntityModels
{
    public class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
