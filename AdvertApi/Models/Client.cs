using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Client
    {
        public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public byte[] Salt { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }
            
    }
}