using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class GroupedSlot : List<Slot>
    {
        public string key { get; private set; }

        public GroupedSlot(string key)
        {
            this.key = key;
            
        }

       
    }
}
