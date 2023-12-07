using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblPosts", Schema = "post")]
    public class Post : BaseEntity
    {
        [Key]
        public Guid PostID { get; set; }
        
        public Guid WriterID { get; set; }
        [ForeignKey(nameof(WriterID))]
        public User Writer { get; set; }

        public string Caption { get; set; }
        public string Content { get; set; }
        public string SeoTags { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set;}

    }
}
