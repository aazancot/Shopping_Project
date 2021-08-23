using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductOrder
    {
        [Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductOrderId { get; set; } 
        public int BarCode { get; set; }     
        public int Count { get; set; }
        public float UnitPrice { get; set; }
        public int StoreId { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; } 
        public bool ProductOrderValidation { get; set; } = false;

        public override string ToString() => $"{ProductOrderId} Store id: {StoreId}\tOrder Date: {Date}\tCount: {Count} \tUnitPrice: {UnitPrice} \tBarCode {BarCode}";

    }
}
