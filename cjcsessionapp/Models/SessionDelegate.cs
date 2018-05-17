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
        public string Pastor { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }

        [DisplayName("Marital Status")]
        public Boolean MartialStatus { get; set; }

        [DisplayName ("Gender")]
        public string Gender { get; set; }

        public string Telephone { get; set; }

        [DisplayName("Require Housing?")]
        public Boolean RequireHousing { get; set; }

        [DisplayName("Emergency Contact Name")]
        public string EmergencyContactName { get; set; }

        [DisplayName("Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }

        [Required]
        [DisplayName("Delegate Of Type")]
        public string DelegateType { get; set; }

        public int InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }

        public Boolean Allergies { get; set; }
        public Boolean Asthma { get; set; }
        public Boolean Diabetes { get; set; }
        public Boolean Vegetarian { get; set; }

        [DisplayName("High Blood Pressure")]
        public Boolean HighBloodPressure { get; set; }

        [DisplayName("Bronchial Disorder")]
        public Boolean BronchialDisorder{ get; set; }

    }
}