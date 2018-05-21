using System.ComponentModel;

namespace cjcsessionapp.Models
{
    public class ReportModel
    {
        [DisplayName("Regular Delegates")]
        public int NumberOfRegularDelegates { get; set; }

        [DisplayName("Delegates At Large")]
        public int NumberOfDelegatesAtLarge { get; set; }

        [DisplayName("Special Delegates")]
        public int NumberOfSpecialDelegates { get; set; }

        [DisplayName("Guests")]
        public int NumberOfGuests { get; set; }

        [DisplayName("Special Guests")]
        public int NumberOfSpecialGuests { get; set; }

        [DisplayName("Total Delegates")]
        public int TotalDelegates { get; set; }

        [DisplayName("Grand Total")]
        public int GrandTotal { get; set; }
    }
}