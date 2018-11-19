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

        public Books(string code, string name, int author, int publisher)
        {
            Isbn = code;
            Nmbook = name;
            Idauthor = author;
            Idpublisher = publisher;
        }
    }
}
