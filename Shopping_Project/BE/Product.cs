using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        [Key] // make it the table key
        public int BarCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
     
        
    }
}
