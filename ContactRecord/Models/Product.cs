using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactRecord.Models
{
    [Table("Product")]
    public class Product
    {
        public long ID { get; set; }

        [DisplayName("Name of Product")]
        public String ProductName { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}