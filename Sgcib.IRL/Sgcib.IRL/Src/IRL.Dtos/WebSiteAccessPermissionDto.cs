using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IRL.Dtos
{
    public class WebSiteAccessPermissionDto
    {
        public int Id { get; set; }

        [Required]
        public Guid RoleId { get; set; }
        
        [Required]
        public string Controller { get; set; }
        
        public string Action { get; set; }

        [Required]
        public bool Enabled { get; set; }
        
        public string Comments { get; set; }

        public RoleDto Role { get; set; }

        public IList<RoleDto> RoleList;

        public IList<string> ControllerList { get; set; }
    }
}
