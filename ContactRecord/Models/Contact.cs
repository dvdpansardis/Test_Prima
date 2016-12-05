using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactRecord.Models
{
    [Table("Contact")]
    public class Contact
    {
        [DisplayName("Code")]
        public long ID { get; set; }

        [DisplayName("Date Time")]
        public DateTime DateTimeContact { get; set; }
        
        [DisplayName("Content")]
        [DataType(DataType.MultilineText)]
        public String Content { get; set; }

        [DisplayName("List of products of interested")]
        public virtual ICollection<Product> Products { get; set; }

        public virtual Institute Institute { get; set; }

        public Contact(){
            DateTimeContact = DateTime.Now;
        }
    }
}
