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
	public partial class StudentNewEventPage : ContentPage
	{
        public string id_professore;
        public string id_corso;
        public string nome_cognome_prof { get; }

		public StudentNewEventPage (string id_prof, string docente)
		{
            BindingContext = this;
            id_professore = id_prof;
            nome_cognome_prof = docente;

            string id_studente = Application.Current.Properties["id_utente"].ToString();
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_corsi_studente_docente.php?" + "id_studente=" + id_studente + "&id_professore=" + id_professore);
            request.DownloadSpinnerCorso(false);

            InitializeComponent ();
		}

        public void fillSpinnerCorso(List<Corso> corsi)
        {

            pickerCorso.ItemsSource = corsi;

            pickerCorso.SelectedIndex = 0;

            id_corso = corsi.First().id_corso;
        }

        protected async void AnnullaClick(object sender, EventArgs args)
        {
            var c = Navigation.NavigationStack.Count();

            for (int i = 0; i < c - 1; i++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[i]);

            }

            await Navigation.PushAsync(new StudentBookSlotPage());
            Navigation.RemovePage(this);
        }

        protected async void RichiediClick(object sender, EventArgs args)
        {

        }


    }
}