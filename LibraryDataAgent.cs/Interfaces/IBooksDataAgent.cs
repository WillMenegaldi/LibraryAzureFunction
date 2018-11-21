using LibraryDataAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryDataAgent.Interfaces
{
    public interface IBooksDataAgent
    {
        List<string[]> Select(string query);
        string ManipulationQuery(string query);
    }
}
