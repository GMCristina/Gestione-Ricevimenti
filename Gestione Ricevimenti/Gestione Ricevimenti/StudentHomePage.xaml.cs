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
		public StudentHomePage ()
		{
            // NavigationPage.SetHasBackButton(this, false);




            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_ricevimenti.php?");
            request.DownloadEvent();


            InitializeComponent ();

           
            NavigationPage.SetHasNavigationBar(this, true);
            
          


          
               // Navigation.RemovePage(Navigation.NavigationStack[0]);
            
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

            await this.Navigation.PushAsync(new LoginPage());

        }

        protected async void Home(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new StudentHomePage());
        }

        protected async void Info(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new InfoPage());
        }
    }
}