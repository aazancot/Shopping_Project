using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product : IComparable
    {
        [Key] // make it the table key
        public int BarCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is Product) || obj == null)
                return -1;
            Product other = obj as Product;
            return BarCode.CompareTo(other.BarCode);
        }
    }
}
