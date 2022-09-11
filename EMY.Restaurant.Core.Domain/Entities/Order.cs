using EMY.Restaurant.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EMY.Restaurant.Core.Domain.Entities
{
    [Table("tblOrder", Schema = "order")]
    public class Order : BaseEntity
    {
        [Key]
        public Guid OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string FullName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullAdress { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool IsSent { get; set; }
        public decimal Discount { get; set; }
        public decimal RealPrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public DateTime AuthorizeDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentAuthorizationToken { get; set; }
        public string Notes { get; set; }
        public DateTime? DestinationTime { get; set; }

        public int GetPercent()
        {
            if (OrderItems == null || OrderItems.Count == 0) return 100;
            var succesitemcount = OrderItems.Count(o => o.IsSuccess);
            var orderItemCount = OrderItems.Count;
            var percent = (int)(((decimal)succesitemcount / (decimal)orderItemCount) * 100);
            return percent;

        }

    }
}
