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
	public partial class StudentHomePage : ContentPage
	{
        public string id_ricevimento;

		public StudentHomePage ()
		{
            

            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti.php?");
            request.DownloadEvent();
            

            List<MenuToolbar> MenuItems = new List<MenuToolbar>
            {
                new MenuToolbar("iu.png","Info"),
                new MenuToolbar("iu.png", "Logout")
            };

            InitializeComponent();

            menuItems.ItemsSource = MenuItems;
            tollbarMenu.IsVisible = false;
       }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti.php?");
            request.DownloadEvent();

        }

        public void fillListStudentHomePage(List<RicevimentoHomePage> l)
        {
            ricevimenti.ItemsSource = l;
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

            await Navigation.PushAsync( new LoginPage());
            Navigation.RemovePage(this);
      
        }

        protected async void Home(object sender, EventArgs e)
        {
           
        }

        protected async void Refresh(object sender, EventArgs e)
        {
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti.php?");
            request.DownloadEvent();
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

        public void EventDetail(object sender, ItemTappedEventArgs e)
        {

            id_ricevimento = ((RicevimentoHomePage)e.Item).id_ricevimento;
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/ricevimento.php?id_ricevimento=" + id_ricevimento);
            request.DownloadEventDetail(true);

        }

        public void Other(object sender, ItemTappedEventArgs e)
        {
            tollbarMenu.IsVisible = true;
        }

        public async void MenuClick(object sender, ItemTappedEventArgs e)
        {
            switch (((MenuToolbar)e.Item).Title)
            {
                case "Info":
                    await Navigation.PushAsync(new InfoPage());
                    break;
                case "Logout":
                    Application.Current.Properties.Remove("id_utente");
                    Application.Current.Properties.Remove("tipo_utente");
                    Application.Current.Properties.Remove("matricola");
                    Application.Current.Properties.Remove("password");
  
                    await Navigation.PushAsync(new LoginPage());
                    Navigation.RemovePage(this); break;

            }
            tollbarMenu.IsVisible = false;

        }

        public void Fine(object sender, ItemTappedEventArgs e)
        {

            popupStudentEvent.IsVisible = false;

        }

        public async void Elimina(object sender, ItemTappedEventArgs e)
        {
            var answer =  await DisplayAlert("Cancella Prenotazione","Confermi di voler cancellare definitivamente la prenotazione effettuata?", "Sì", "No");
            if (answer)
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/cancella_ricevimento.php?id=" + id_ricevimento);
                request.DeleteEvent();

                popupStudentEvent.IsVisible = false;
             
            }

        }

        protected async void BookSlot(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StudentBookSlotPage());
        

        }

        public void WriteDetail (RicevimentoHomePage r)
        {
            dataDocente.Text = r.nome_cognome;
            dataCorso.Text = r.corso;
            dataData.Text = r.giorno;
            dataInizio.Text = r.inizio;
            dataFine.Text = r.fine;
            dataOggetto.Text = r.oggetto;
           

          popupStudentEvent.IsVisible = true;

        }

       

    }
}