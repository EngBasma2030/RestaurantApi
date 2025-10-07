using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string? email)
            : base(u => string.IsNullOrEmpty(email) || u.Email == email)
        {
        }
    }
}
