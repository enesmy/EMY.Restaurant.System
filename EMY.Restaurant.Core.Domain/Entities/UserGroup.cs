using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblUserGroups", Schema = "authorize")]
    public class UserGroup : BaseEntity
    {
        [Key]
        public Guid UserGroupID { get; set; }
        public string UserGroupCode { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupToolTip { get; set; }
        public bool DefaultUserGroup { get; set; }
        public ICollection<UserGroupRole> Roles { get; set; }
        public bool IsActive { get; set; }

        public string GetGroupName() => $"{UserGroupName} ({UserGroupCode})";
    }
}