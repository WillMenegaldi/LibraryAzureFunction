using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryDataAgent.Models
{
    public class Books
    {
        public string Isbn { get; set; }
        public string Nmbook { get; set; }
        public int Idauthor { get; set; }
        public int Idpublisher { get; set; }
    }
}
