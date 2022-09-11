using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblPhoto", Schema = "dbo")]
    public class Photo : BaseEntity
    {

        [Key]
        public Guid PhotoID { get; set; }
        public string FileName { get; set; }
        public string Extention { get; set; }



    }
}
