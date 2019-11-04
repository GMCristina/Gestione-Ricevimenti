using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class RicevimentoHomePageResp
    {
        public RicevimentoHomePageResp(string id_ricevimento, string nome, string cognome, string giorno, string inizio, string fine, string corso, string stato)
        {
            this.id_ricevimento = id_ricevimento;
            this.nome = nome;
            this.cognome = cognome;
            this.giorno = giorno;
            this.inizio = inizio;
            this.fine = fine;
            this.corso = corso;
            this.stato = stato;
        }

        public string id_ricevimento { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string giorno { get; set; }
        public string inizio { get; set; }
        public string fine { get; set; }
        public string corso { get; set; }
        public string stato { get; set; }


    }
}
