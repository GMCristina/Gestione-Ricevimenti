using System;
using System.Collections.Generic;
using System.Text;

namespace Gestione_Ricevimenti
{
    public interface IMessage
    {
        void ShortAlert(string msg);
    }
}
