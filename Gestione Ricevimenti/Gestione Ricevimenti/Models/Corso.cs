using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class Corso
    {
        public string id_corso { set; get; }
        public string nome { set; get; }

        public Corso(string id_corso, string nome)
        {
            this.id_corso = id_corso;
            this.nome = nome;
        }
    }
}
