using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Gestione_Ricevimenti
{
    class CheckConnection
    {
        static public bool CheckInternetConnection( Page p)
        {
            if (NetworkAccess.Internet == Connectivity.NetworkAccess)
            {
                return true;
            }
            else
            {
                p.DisplayAlert("Connessione Internet assente", "Attiva la connesione dati e riprova", "Riprova");
                return false;
            }
        }
    }
}
