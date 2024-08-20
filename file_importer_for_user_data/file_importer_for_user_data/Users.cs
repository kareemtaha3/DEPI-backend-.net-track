using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace file_importer_for_user_data
{
    public class Users
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHashed { get; set; }
        public string? Status { get; set; }
        public string? AddressLine1 { get; set;}
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set;}
        public int? CountryID { get; set; }
        public DateTime? CreatedAT { get; set; }

        public Users(int userID, string? username, string? email, string? passwordHashed, string? status, string? addressLine1,
                    string? addressLine2, string? city, string? state, string? postalCode, int? countryID, DateTime? createdAT)
        {
            UserID = userID;
            Username = username;
            Email = email;
            PasswordHashed = passwordHashed;
            Status = status;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            CountryID = countryID;
            CreatedAT = createdAT;
        }

    }
}
