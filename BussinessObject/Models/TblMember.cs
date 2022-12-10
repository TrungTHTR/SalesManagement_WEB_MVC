using System;
using System.Collections.Generic;

#nullable disable

namespace BussinessObject.Models
{
    public partial class TblMember
    {
        public TblMember()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public TblMember(int memberid, string email, string password, string companyName, string city, string country)
        {
            MemberId = memberid;
            Email = email;
            Password = password;
            CompanyName = companyName;
            City = city;
            Country = country;
        }

        public int MemberId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
