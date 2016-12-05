using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactRecord.Models
{
    [Table("Institute")]
    public class Institute
    {
        [DisplayName("Code")]
        public long ID { get; set; }

        [DisplayName("Name of Institute")]
        public String InstituteName { get; set; }

        [DisplayName("Name of Contact")]
        public String ContactName { get; set; }

        [DisplayName("Telephone number of Contact")]
        public String TelephoneNumber { get; set; }
        
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}