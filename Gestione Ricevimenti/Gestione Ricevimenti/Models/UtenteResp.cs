using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public class UtenteResp
    {
        public string id { get; set; }
        public string tipo { get; set; }

        public UtenteResp(string id, string tipo)
        {
            this.id = id;
            this.tipo = tipo;
        }
    }

}
