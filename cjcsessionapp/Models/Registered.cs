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
        public DateTime DateAndTime { get; set; }
    }
}