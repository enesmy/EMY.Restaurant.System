using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblMailList", Schema = "mail")]
    public class MailList : BaseEntity
    {
        [Key]
        public Guid MailListID { get; set; }
        public string Email { get; set; }
    }
}
