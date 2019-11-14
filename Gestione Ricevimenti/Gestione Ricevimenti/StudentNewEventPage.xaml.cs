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
	public partial class StudentNewEventPage : ContentPage
	{
        public string id_professore;
        public string id_corso;
        public string nome_cognome_prof { get; }
        public string durata { set; get; }
        public DateTime giorno { set; get; }
        public TimeSpan inizio { set; get; }
        public string oggetto;

        public StudentNewEventPage (string id_prof, string docente)
		{
            BindingContext = this;
            id_professore = id_prof;
            nome_cognome_prof = docente;

            string id_studente = Application.Current.Properties["id_utente"].ToString();

                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/elenco_corsi_studente_docente.php?" + "id_studente=" + id_studente + "&id_professore=" + id_professore);
                request.DownloadSpinnerCorso(false);
            

            InitializeComponent ();

            Timepick.Time = DateTime.Now.TimeOfDay;
            inizio = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
            Datepick.Date = DateTime.Now.Date;
            Datepick.MinimumDate = DateTime.Now.Date; 
            giorno = DateTime.Now.Date;

            pickerDurata.SelectedIndex = 0;
            durata = pickerDurata.SelectedItem.ToString();

           
            

            oggetto = "";

        }

        public void fillSpinnerCorso(List<Corso> corsi)
        {

            pickerCorso.ItemsSource = corsi;

            pickerCorso.SelectedIndex = 0;

            id_corso = corsi.First().id_corso;
        }

        public async void AnnullaClick(object sender, EventArgs args)
        {
          
            await Navigation.PopAsync();
        }

        protected async void RichiediClick(object sender, EventArgs args)
        {
            if (id_professore != null && id_corso != null && giorno != null && inizio != null && durata != null)
            {
                string durataMod;
                String[] st = durata.Split(' ');
                if (st[1].Equals("h"))    
                    durataMod = ((Convert.ToInt32(st[0])) * 60).ToString();
                else
                    durataMod = st[0];

                oggetto = Note.Text;

                id_corso = ((Corso)pickerCorso.SelectedItem).id_corso;

                string inizioMod = inizio.Hours + ":" + inizio.Minutes;
                string giornoMod = giorno.Day + "/" + giorno.Month + "/" + giorno.Year;

                if (CheckConnection.CheckInternetConnection(this))
                {
                    ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/richiesta_nuovo_ricevimento.php");
                    request.SlotRequest(id_professore, id_corso, giornoMod, inizioMod, durataMod, oggetto);
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Richiesta fallita: dati mancanti");
            }
        }

      
    }
}