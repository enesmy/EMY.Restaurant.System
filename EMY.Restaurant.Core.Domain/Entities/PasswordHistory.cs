using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblPasswordHistories"), Index(nameof(UserID))]
    public class PasswordHistory : BaseEntity
    {
        public PasswordHistory()
        {

        }
        public PasswordHistory(Guid UserID, string PasswordHash)
        {
            this.UserID = UserID;
            this.PasswordHash = PasswordHash;
            this.PasswordDate = DateTime.Now;
        }
        [Key] public Guid PasswordHistoryID { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID"), NotMapped] public virtual User Profile { get; set; }
        public string PasswordHash { get; set; }
        public DateTime PasswordDate { get; set; }
    }
}
