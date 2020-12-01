using System;
using System.Collections.Generic;
using System.Text;

namespace MangoOfficeClient.Users
{
    public class General
    {
        public string name { get; set; }
        public string email { get; set; }
        public string department { get; set; }
        public string position { get; set; }
    }

    public class Number
    {
        public string number { get; set; }
        public string number_normalized { get; set; }
        public string protocol { get; set; }
        public int order { get; set; }
        public int? wait_sec { get; set; }
        public string status { get; set; }
    }

    public class Telephony
    {
        public string extension { get; set; }
        public string outgoingline { get; set; }
        public List<Number> numbers { get; set; }
    }

    public class User
    {
        public General general { get; set; }
        public Telephony telephony { get; set; }
    }
    public class RootUser
    {
        public List<User> users { get; set; }
    }

}
