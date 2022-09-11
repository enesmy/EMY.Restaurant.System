using System;
using System.ComponentModel.DataAnnotations;

namespace EMY.Restaurant.Core.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid CreatorID { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public Guid LastUpdaterID { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeleterID { get; set; }
    }
}