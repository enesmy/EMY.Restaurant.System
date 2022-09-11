using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblUserGroupRoles", Schema = "authorize")]
    public class UserGroupRole : BaseEntity
    {
        [Key]
        public Guid UserGrpoupRoleID { get; set; }
        public Guid UserGroupID { get; set; }
        public string FormName { get; set; }
        public AuthType AuthorizeType { get; set; }
        [ForeignKey("UserGroupID")] public virtual UserGroup UserGroup { get; set; }

    }
}