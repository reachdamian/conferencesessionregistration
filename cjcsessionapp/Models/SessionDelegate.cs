using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cjcsessionapp.Models
{
    public class SessionDelegate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Title { get; set; }


        public string Email { get; set; }

        public string Age { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }

        [DisplayName("Require Housing?")]
        public Boolean RequireHousing { get; set; }

        [DisplayName("Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [DisplayName("Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }

        [Required]
        [DisplayName("Delegate Type")]
        public string DelegateType { get; set; }
    }
}