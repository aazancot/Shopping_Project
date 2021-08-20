using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Url
    {

        [Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         // make it the table key
         public int UrlId { get; set; }
        public string DownloadUrl { get; set; }
    }
}
