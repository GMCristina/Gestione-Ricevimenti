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
	public partial class ProfInsertSlotPage : ContentPage
	{
        public string durata { set; get; }
        public string durataSlot { get; set; }
        public DateTime giorno { set; get; }
        public TimeSpan inizio { set; get; }

        public ProfInsertSlotPage ()
		{
            BindingContext = this;

            InitializeComponent ();

            pickerDurata.SelectedIndex = 0;
            pickerDurataSlot.SelectedIndex = 0;

            durata = pickerDurata.SelectedItem.ToString();
            durataSlot = pickerDurataSlot.SelectedItem.ToString();

        


            Timepick.Time = DateTime.Now.TimeOfDay;
            inizio = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
            Datepick.Date = DateTime.Now.Date;
            Datepick.MinimumDate = DateTime.Now.Date;
            giorno = DateTime.Now.Date;

        }



        public async void AnnullaClick(object sender, EventArgs args)
        {

            await Navigation.PopAsync();
        }

        protected async void InserisciClick (object sender, EventArgs args)
        {
            if ( giorno != null && inizio != null && durata != null && durataSlot!=null)
            {
                string durataMod;
                String[] st = durata.Split(' ');
                if (st[1].Equals("h"))
                {
                    durataMod = ((Convert.ToInt32(st[0])) * 60).ToString();
                }
                else
                {
                    durataMod = st[0];
                }

                string durataSlotMod;
                st = durataSlot.Split(' ');
                if (st[1].Equals("h"))
                {
                    durataSlotMod = ((Convert.ToInt32(st[0])) * 60).ToString();
                }
                else
                {
                    durataSlotMod = st[0];
                }


                string inizioMod = inizio.Hours + ":" + inizio.Minutes;
                string giornoMod = giorno.Day + "-" + giorno.Month + "-" + giorno.Year;
                
                if (CheckConnection.CheckInternetConnection(this))
                {
                    ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/inserimento_slot.php");
                    request.InsertSlot(giornoMod, inizioMod, durataMod, durataSlotMod);
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Richiesta fallita: dati mancanti");
            }
        }


    }
}