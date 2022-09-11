using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblOrderItem", Schema = "order")]
    public class OrderItem : BaseEntity
    {
        [Key]
        public Guid OrderItemID { get; set; }
        public Guid OrderID { get; set; }
        [ForeignKey("OrderID")] public Order Order { get; set; }
        public Guid MenuID { get; set; }
        public string MenuText { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemPrice { get; set; }
        public virtual Decimal TotalPrice { get { return Math.Round(ItemCount * ItemPrice, 2); } }
        public bool IsSuccess { get; set; }

    }
}
