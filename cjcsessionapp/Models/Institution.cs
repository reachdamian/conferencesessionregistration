﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cjcsessionapp.Models
{

    public class Institution
    {
        public Institution()
        {
            SessionDelegate = new List<SessionDelegate>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int NumberOfDelegatesAssigned { get; set; }
        
        public virtual List<SessionDelegate> SessionDelegate { get; set; }
    }
}