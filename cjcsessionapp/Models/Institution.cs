using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cjcsessionapp.Models
{

    public class Institution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int NumberOfDelegatesAssigned { get; set; }
        
        public int DelegateId { get; set; }
        public virtual SessionDelegate SessionDelegate { get; set; }
    }
}