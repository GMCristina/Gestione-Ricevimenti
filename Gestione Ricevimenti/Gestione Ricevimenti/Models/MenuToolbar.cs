using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    class MenuToolbar
    {

        public string Source { get; set; }
        public string Title { get; set; }
        public string Description { get; set;}
        
        public MenuToolbar(string source, string title)
        {
            Source = source;
            Title = title;
            Description = "";
           
        }
    }


}
