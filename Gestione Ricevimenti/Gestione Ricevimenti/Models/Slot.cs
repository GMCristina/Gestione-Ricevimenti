using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class Slot
    {
        public string id_professore { get; set;}
        public string id_ricevimento { get; set; }
        public string giorno { get; set; }
        public string inizio { get; set; }
        public string fine { get; set; }

        public Slot(string id_professore, string id_ricevimento, string giorno, string inizio, string fine)
        {
            this.id_professore = id_professore;
            this.id_ricevimento = id_ricevimento;
            this.giorno = giorno;
            this.inizio = inizio;
            this.fine = fine;
        }

        public string inizio_fine
        {
            get
            {
                return inizio + " : " + fine;
            }
        }

    }
}
