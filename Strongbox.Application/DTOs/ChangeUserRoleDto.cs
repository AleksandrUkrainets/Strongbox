using Strongbox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strongbox.Application.DTOs
{
    public class ChangeUserRoleDto
    {
        public Guid UserId { get; set; }
        public PersonRole Role { get; set; }
    }
}
