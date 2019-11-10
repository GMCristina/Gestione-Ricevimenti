using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    class GroupedRicevimento : List<RicevimentoHomePage>
    {
        public string key { get; private set; }

        public GroupedRicevimento(string key)
        {
            this.key = key;

        }

    }
}
