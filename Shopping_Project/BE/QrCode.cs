using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class QrCode
    {
        [Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QrCodeId { get; set; }
        public string StoreName { get; set; }
        public string StoreCity { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public float UnitPrice { get; set; }
        public int BarCode { get; set; }
        public DateTime Date { get; set; }
    }

}
