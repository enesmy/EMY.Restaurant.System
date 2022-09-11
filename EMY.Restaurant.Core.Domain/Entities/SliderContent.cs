using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblSliders", Schema = "dbo")]
    public class SliderContent : BaseEntity
    {
        [Key]
        public Guid SliderContentID { get; set; }
        public int SliderIndex { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string ImgUrl { get; set; }
        public string Link { get; set; }
        public ContentSide Side { get; set; }

    }
    
}
