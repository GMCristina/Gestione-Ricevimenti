using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gestione_Ricevimenti
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfHomePage : ContentPage
	{
        string categoria { set; get; }
        string id_ricevimento;
        List<RicevimentoHomePage> lista;
        List<GroupedRicevimento> listaGrouped;

		public ProfHomePage ()
        {
            string id = Application.Current.Properties["id_utente"].ToString();

            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti_prof.php?" + "id_professore=" + id);
                request.DownloadSlotProf();
            }

            InitializeComponent ();

            pickerStato.SelectedIndex = 0;
            categoria = pickerStato.SelectedItem.ToString();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            pickerStato.SelectedIndex = 0;
            string id = Application.Current.Properties["id_utente"].ToString();
            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti_prof.php?" + "id_professore=" + id);
                request.DownloadSlotProf();
            }

        }


        protected async void Logout(object sender, EventArgs args)
        {
            Application.Current.Properties.Remove("id_utente");
            Application.Current.Properties.Remove("tipo_utente");
            Application.Current.Properties.Remove("matricola");
            Application.Current.Properties.Remove("password");

            var c = Navigation.NavigationStack.Count();

            for (int i = 0; i < c - 1; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);

            }

            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);

        }

        protected async void Home(object sender, EventArgs e)
        {

        }

        public async void Refresh(object sender, EventArgs e)
        {
            pickerStato.SelectedIndex = 0;
            string id = Application.Current.Properties["id_utente"].ToString();

            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti_prof.php?" + "id_professore=" + id);
                request.DownloadSlotProf();
            }
        }

        protected async void Info(object sender, EventArgs e)
        {
            var c = Navigation.NavigationStack.Count();

            for (int i = 0; i < c - 1; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);

            }

            await Navigation.PushAsync(new InfoPage());
        }


        public void fillListProfHomePage(List<RicevimentoHomePage> list, bool ModificaSource)
        {
            

            if (ModificaSource)
            {
                lista = list;
            }

            List<GroupedRicevimento> l = new List<GroupedRicevimento>();

            if (list.Count() != 0)
            {
                string precGiorno = list.First().giorno;
                GroupedRicevimento l1 = new GroupedRicevimento(precGiorno);



                foreach (RicevimentoHomePage elem in list)
                {
                    if (precGiorno.Equals(elem.giorno))
                    {
                        l1.Add(elem);
                    }
                    else
                    {
                        l.Add(l1);
                        precGiorno = elem.giorno;
                        l1 = new GroupedRicevimento(precGiorno);
                        l1.Add(elem);
                    }

                }

                l.Add(l1);

                ricevimenti.ItemsSource = l;

                if (ModificaSource)
                {
                    listaGrouped = l;
                }

            }
            else
            {

                ricevimenti.ItemsSource = null;
            }
        }

        protected async void NewSlot(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ProfInsertSlotPage());


        }

        private void ChangePickerStato(object sender, EventArgs e)
        {
            

            List<GroupedRicevimento> l = new List<GroupedRicevimento>();

            categoria = pickerStato.SelectedItem.ToString();

            string cat = "";

            switch (categoria)
            {
                case "Tutti": cat = categoria; break;
                case "Liberi": cat = "Libero"; break;
                case "Prenotati": cat = "Prenotato"; break;
                case "Richiesti": cat = "Richiesto"; break;
                case "Approvati": cat = "Approvato"; break;
                case "Eliminati": cat = "Eliminato"; break;

            }

            if (lista!=null)
            {
                

                if (cat.Equals("Tutti"))
                {
                    ricevimenti.ItemsSource = listaGrouped;

                }
                else
                {
                    List<RicevimentoHomePage> l1 = new List<RicevimentoHomePage>();

                    foreach (RicevimentoHomePage elem in lista)
                    {
                        if(cat.Equals(elem.stato_stringa))
                        {
                            l1.Add(elem);
                        }
                        
                    }

                    fillListProfHomePage(l1,false);

                   
                }


            }
            else
            {

                ricevimenti.ItemsSource = null;
            }



        }


        public void EventDetail(object sender, ItemTappedEventArgs e)
        {

            id_ricevimento = ((RicevimentoHomePage)e.Item).id_ricevimento;
            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/ricevimento.php?id_ricevimento=" + id_ricevimento);
                request.DownloadEventDetail(false);
            }

        }


        public void WriteDetail(RicevimentoHomePage r)
        {

            switch (r.stato)
            {
                case "0":
                    labelStudente.IsVisible = false;
                    dataStudente.IsVisible= false;
                    labelCorso.IsVisible=false;
                    dataCorso.IsVisible = false;
                    labelOggetto.IsVisible = false;
                    dataOggetto.IsVisible = false;
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;

                    Elimina.IsVisible = true;
                    Fine.IsVisible = true;
                    break;

                case "1":
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;

                    Elimina.IsVisible = true;
                    Fine.IsVisible = true;
                    labelStudente.IsVisible = true;
                    dataStudente.IsVisible = true;
                    labelCorso.IsVisible = true;
                    dataCorso.IsVisible = true;
                    labelOggetto.IsVisible = true;
                    dataOggetto.IsVisible = true;

                    break;

                case "2":
                    Elimina.IsVisible = false;

                    Conferma.IsVisible = true;
                    Rifiuta.IsVisible = true;
                    Fine.IsVisible = true;
                    labelStudente.IsVisible = true;
                    dataStudente.IsVisible = true;
                    labelCorso.IsVisible = true;
                    dataCorso.IsVisible = true;
                    labelOggetto.IsVisible = true;
                    dataOggetto.IsVisible = true;


                    break;

                case "3":
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;
                    break;

                case "4":
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;
                    Elimina.IsVisible = true;
                    Fine.IsVisible = true;

                    labelStudente.IsVisible = true;
                    dataStudente.IsVisible = true;
                    labelCorso.IsVisible = true;
                    dataCorso.IsVisible = true;
                    labelOggetto.IsVisible = true;
                    dataOggetto.IsVisible = true;

                    break;
                case "5":
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;
                    Elimina.IsVisible = true;
                    Fine.IsVisible = true;

                    labelStudente.IsVisible = true;
                    dataStudente.IsVisible = true;
                    labelCorso.IsVisible = true;
                    dataCorso.IsVisible = true;
                    labelOggetto.IsVisible = true;
                    dataOggetto.IsVisible = true;
                    break;

                case "6":
                    Conferma.IsVisible = false;
                    Rifiuta.IsVisible = false;
                    break;

                default:
                    break;
            }
            

            dataData.Text = r.giorno;
            dataInizio.Text = r.inizio;
            dataFine.Text = r.fine;

            dataStudente.Text = r.nome_cognome;
            dataCorso.Text = r.corso;
           
            dataOggetto.Text = r.oggetto;


            popupProfEvent.IsVisible = true;

        }

        public void FineClick(object sender, ItemTappedEventArgs e)
        {

            popupProfEvent.IsVisible = false;

        }

        public void EliminaClick(object sender, ItemTappedEventArgs e)
        {
            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/gestione_ricevimento.php?" + "id=" + id_ricevimento + "&azione=" + "Elimina");
                request.HandleEvent();
            }


            popupProfEvent.IsVisible = false;

        }

        public void RifiutaClick(object sender, ItemTappedEventArgs e)
        {
            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/gestione_ricevimento.php?" + "id=" + id_ricevimento + "&azione=" + "Rifiuta");
                request.HandleEvent();
            }

            popupProfEvent.IsVisible = false;

        }

        public void ConfermaClick(object sender, ItemTappedEventArgs e)
        {
            if (CheckConnection.CheckInternetConnection(this))
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/gestione_ricevimento.php?" + "id=" + id_ricevimento + "&azione=" + "Conferma");
                request.HandleEvent();
            }

            popupProfEvent.IsVisible = false;

        }
    }
}