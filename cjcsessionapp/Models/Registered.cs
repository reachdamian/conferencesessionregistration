using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cjcsessionapp.Models
{
    public class Registered
    {
        public int RegisteredId { get; set; }

        public int SessionDelegateId { get; set; }
        public virtual SessionDelegate SessionDelegate { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime DateAndTime { get; set; }
    }
}