using EMY.Restaurant.Core.Domain.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblUsers", Schema = "authorize")]
    public class User : BaseEntity
    {
        [Key]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }

        [DefaultValue(0)]
        public UserStatus UserStatus { get; set; }
        public Guid UserGroupID { get; set; }
        [ForeignKey("UserGroupID")] public UserGroup Group { get; set; }

        public string PasswordStored { get; set; }

        [NotMapped]
        public string Password
        {
            set { PasswordStored = SystemCryptography.Encrypt(value); }
        }

        public string HiddenQuestionAnswer { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockedTime { get; set; }
        public int WrongForceCount { get; set; }
        public DateTime? LastWrongTryingTime { get; set; }
        public string HiddenQuestion { get; set; }
        public string Notes { get; set; }

        public string GetFullName()
        {
            return Name + " " + LastName;
        }
        public bool PasswordControl(string password)
        {
            string pswhash = SystemCryptography.Encrypt(password);
            return (pswhash == PasswordStored);
        }


    }
}
