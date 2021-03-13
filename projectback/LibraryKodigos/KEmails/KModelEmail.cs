using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryKodigos.KEmails
{
    public class KModelEmail
    {
        public string STMP { get; set; }
        public int Port { get; set; }
        public string EmailFrom { get; set; }
        public KUserEmail From { get; set; }
        public List<KUserEmail> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = false;
    }
}
