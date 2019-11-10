using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class RicevimentoHomePage
    {
        public RicevimentoHomePage(string id_ricevimento, string nome, string cognome, string giorno, string inizio, string fine, string corso, string stato, string oggetto)
        {
            this.id_ricevimento = id_ricevimento;
            this.nome = nome;
            this.cognome = cognome;
            this.giorno = giorno;
            this.inizio = inizio;
            this.fine = fine;
            this.corso = corso;
            this.stato = stato;
            this.oggetto = oggetto;
        }

        public string id_ricevimento { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string giorno { get; set; }
        public string inizio { get; set; }
        public string fine { get; set; }
        public string corso { get; set; }
        public string stato { get; set; }
        public string oggetto { get; set; }

        public string id_professore { get; set; }

        public string stato_stringa {
            get {
                string s = "";
                switch (stato)
                {
                    case "2": // richiesto
                        s = "Richiesto";
                        break;
                    case "3": // rifiutato
                        s = "Rifiutato";
                        break;
                    case "1": // confermato
                        s = "Prenotato";
                        break;
                    case "4": // confermato
                        s = "Approvato";
                        break;
                    case "0": // libero
                        s = "Libero";
                        break;
                    case "5":
                        s = "Eliminato"; //eliminato da studente
                        break;

                    case "6":
                        s = "Deprecato";
                        break;
                }
                return s;
            }
        }

        public string nome_cognome
        {
            get
            {
                if (nome != null && cognome != null)
                    return nome + " " + cognome;
                else
                    return "Nessuno studente";
            }
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
