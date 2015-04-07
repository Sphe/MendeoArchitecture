using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IRL.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }

        [Required]
        public string ADName { get; set; }

        public string DisplayName { get; set; }
    }
}
