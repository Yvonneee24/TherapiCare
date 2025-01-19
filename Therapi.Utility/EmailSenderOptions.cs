using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Therapi.Utility
{
    public class EmailSenderOptions
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public SmtpCredentialOptions SmtpCredential { get; set; }
    }

    public class SmtpCredentialOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
