using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    class Ricevimento
    {
        public string id_ricevimento { get; set; }
        public string id_docente { get; set; }
        public string id_studente { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string snome { get; set; }
        public string scognome { get; set; }
        public string giorno { get; set; }
        public string inizio { get; set; }
        public string fine { get; set; }
        public string corso { get; set; }
        public string oggetto { get; set; }
        public string stato { get; set; }
    }
}
