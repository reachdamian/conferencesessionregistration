using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cjcsessionapp.Models
{
    public class SessionDelegate
    {
        public SessionDelegate()
        {
            Registered = new List<Registered>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Title { get; set; }        
        public string Address { get; set; }
        public string Email { get; set; }
        public string AgeGroup { get; set; }

        [DisplayName("Marital Status")]
        public string MartialStatus { get; set; }

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
        
        public Boolean Allergies { get; set; }
        public Boolean Asthma { get; set; }
        public Boolean Diabetes { get; set; }
        public Boolean Vegetarian { get; set; }

        [DisplayName("High Blood Pressure")]
        public Boolean HighBloodPressure { get; set; }

        [DisplayName("Bronchial Disorder")]
        public Boolean BronchialDisorder{ get; set; }

        public int InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }

        public virtual List<Registered> Registered { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}