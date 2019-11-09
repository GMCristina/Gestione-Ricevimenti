using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gestione_Ricevimenti
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentBookSlotPage : ContentPage
	{
        public string id_professore;
        public string id_ricevimento;
        public string id_corso;
        public string nome_cognome_prof;

		 public StudentBookSlotPage ()
		{
           
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/professori.php?");
            request.DownloadSpinnerDocente();

            InitializeComponent();
  
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            
        }

        public void fillSpinnerDocente(List<Docente> docenti)
        {
            id_professore = docenti.First().id_professore;
            nome_cognome_prof = docenti.First().nome_cognome;
            
            ServerRequest request1 = new ServerRequest(this, "http://pmapp.altervista.org/ricevimenti_liberi.php?id_professore=" + id_professore);
            request1.DownloadSlot();

            pickerDocente.ItemsSource = docenti;

            pickerDocente.SelectedIndex = 0;

        }



        public void fillListSlots(List<Slot> list)
        {
            
            List<GroupedSlot> l = new List<GroupedSlot>();

            if (list.Count()!=0)
            {
                string precGiorno = list.First().giorno;
                GroupedSlot l1 = new GroupedSlot(precGiorno);



                foreach (Slot elem in list)
                {
                    if (precGiorno.Equals(elem.giorno))
                    {
                        l1.Add(elem);
                    }
                    else
                    {
                        l.Add(l1);
                        precGiorno = elem.giorno;
                        l1 = new GroupedSlot(precGiorno);
                        l1.Add(elem);
                    }

                }

                l.Add(l1);

                slots.ItemsSource = l;

            }
            else
            {

                slots.ItemsSource = null;
            }
            
        }

        private void DocenteSelezionato(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            id_professore = ((Docente)picker.SelectedItem).id_professore;

            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/ricevimenti_liberi.php?id_professore=" + id_professore);
            request.DownloadSlot();

        }



        protected async void Logout(object sender, EventArgs args)
        {
            Application.Current.Properties.Remove("id_utente");
            Application.Current.Properties.Remove("tipo_utente");
            Application.Current.Properties.Remove("matricola");
            Application.Current.Properties.Remove("password");



            var c = Navigation.NavigationStack.Count();
            
            for (int i = 0; i< c-1 ; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);
          
            }

            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);


        }

        protected async void Home(object sender, EventArgs e)
        {
            var c = Navigation.NavigationStack.Count();

            for (int i = 0; i < c - 1; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);

            }

            await Navigation.PushAsync(new StudentHomePage());
            Navigation.RemovePage(this);
        }

        protected async void Refresh(object sender, EventArgs e)
        {
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/ricevimenti_liberi.php?id_professore=" + id_professore);
            request.DownloadSlot();
        }

        protected async void Info(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }


        protected async void NonPrenota(object sender, EventArgs args)
        {
            popupBookSlot.IsVisible = false;

        }

        protected async void Prenota(object sender, EventArgs args)
        {

            string id_studente = Application.Current.Properties["id_utente"].ToString();

            if (id_ricevimento!=null && id_professore!=null && id_corso!=null) {
                ServerRequest request = new ServerRequest(this, ("http://pmapp.altervista.org/prenota_slot.php?" + "id_studente=" + id_studente + "&id_corso=" + id_corso + "&id_ricevimento=" + id_ricevimento + "&id_docente=" + id_professore + "&oggetto=" + Oggetto.Text));
                request.BookSlot(id_professore);

              //  ServerRequest request2 = new ServerRequest(this, "http://pmapp.altervista.org/ricevimenti_liberi.php?id_professore=" + id_professore);
               // request2.DownloadSlot();



                popupBookSlot.IsVisible = false;
            }
            else
            {
                popupBookSlot.IsVisible = false;
                DependencyService.Get<IMessage>().ShortAlert("Prenotazione fallita: dati mancanti");
            }


        }

    

        public void fillSpinnerCorso(List<Corso> corsi)
        {

            pickerCorso.ItemsSource = corsi;

            pickerCorso.SelectedIndex = 0;

            id_corso = corsi.First().id_corso;
        }

        

        private void ItemClick(object sender, ItemTappedEventArgs e)
        {
            id_ricevimento = ((Slot)e.Item).id_ricevimento;
            string id_studente = Application.Current.Properties["id_utente"].ToString();
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_corsi_studente_docente.php?" + "id_studente=" + id_studente + "&id_professore=" + id_professore);
            request.DownloadSpinnerCorso(true);

            popupBookSlot.IsVisible = true;

    }

      

        private void NuovoRicevimento(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StudentNewEventPage(id_professore, nome_cognome_prof));
        }
    }
}