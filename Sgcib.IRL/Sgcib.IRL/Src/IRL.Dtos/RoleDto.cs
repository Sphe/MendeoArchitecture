using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IRL.Dtos
{
    public class RoleDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
