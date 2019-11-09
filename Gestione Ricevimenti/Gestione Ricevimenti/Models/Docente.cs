using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class Docente
    {
        public string id_professore { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }

        public Docente(string id_professore, string nome, string cognome)
        {
            this.id_professore = id_professore;
            this.nome = nome;
            this.cognome = cognome;
        }

        public string nome_cognome
        {
            get
            {
                return nome + " " + cognome;
            }
        }
    }
}
