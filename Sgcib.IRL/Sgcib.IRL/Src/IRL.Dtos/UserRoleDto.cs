using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IRL.Dtos
{
    public class UserRoleDto
    {
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public UserDto User { get; set; }

        public RoleDto Role { get; set; }

        public IList<UserDto> UserList { get; set; }

        public IList<RoleDto> RoleList { get; set; }
    }
}
