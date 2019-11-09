using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Gestione_Ricevimenti
{
    class ServerRequest
    {
        private string URL;
        private HttpClient _client;
        private Page mainPage;

        public ServerRequest(Page mainPage, string URL)
        {
            this.mainPage = mainPage;
            this.URL = URL;
            this._client = new HttpClient();
        }

      

        public async void LoginRequest(string user, string psw)
        {
            HttpContent formcontent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("matricola",user),
                new KeyValuePair<string,string>("password",psw)
            });

            var response = await _client.PostAsync(URL, formcontent);
            if (response.IsSuccessStatusCode)
            {
                string responseText = response.Content.ReadAsStringAsync().Result.ToString();
                Debug.WriteLine("res: " + responseText);
              
                if (!responseText.Equals("[]"))
                {
                    Dictionary<string, UtenteResp> utente = JsonConvert.DeserializeObject<Dictionary<string, UtenteResp>>(responseText);
                    foreach (KeyValuePair<string, UtenteResp> elem in utente)
                    {
                        ((LoginPage)mainPage).SaveUtente(elem.Value);
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Login fallito, matricola o/e password errate!");
                }
                

            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Login fallito, riprova");
            }
        

        }

        public async void DownloadEvent()
        {
            string id = Application.Current.Properties["id_utente"].ToString();
            string url = URL + "id=" + id;

            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, RicevimentoHomePage> result_event = JsonConvert.DeserializeObject<Dictionary<string, RicevimentoHomePage>>(result);
                List<RicevimentoHomePage> events = new List<RicevimentoHomePage>();

                foreach (KeyValuePair<string, RicevimentoHomePage> elem in result_event)
                {
                 
                    events.Add(new RicevimentoHomePage(elem.Value.id_ricevimento, elem.Value.nome, elem.Value.cognome, elem.Value.giorno, elem.Value.inizio, elem.Value.fine, elem.Value.corso, elem.Value.stato, elem.Value.oggetto));
                }
                ((StudentHomePage)mainPage).fillListStudentHomePage(events);
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");


            
        }

        public async void DownloadEventDetail()
        {
           

            var response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, RicevimentoHomePage> result_event = JsonConvert.DeserializeObject<Dictionary<string, RicevimentoHomePage>>(result);
                List<RicevimentoHomePage> events = new List<RicevimentoHomePage>();

                foreach (KeyValuePair<string, RicevimentoHomePage> elem in result_event)
                {

                    events.Add(new RicevimentoHomePage(elem.Value.id_ricevimento, elem.Value.nome, elem.Value.cognome, elem.Value.giorno, elem.Value.inizio, elem.Value.fine, elem.Value.corso, elem.Value.stato, elem.Value.oggetto));
                }
                ((StudentHomePage)mainPage).WriteDetail(events[0]);
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");
        }




        public async void DeleteEvent()
        {
            var response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result.ToString();
                string res = JsonConvert.DeserializeObject<string>(result);
               if(res.Equals("-1"))
                {
                    Debug.WriteLine("Errore cancellazione");
                } else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Cancellazione effettuata!");
                    URL = "http://pmapp.altervista.org/elenco_ricevimenti.php?";
                    DownloadEvent();
                }
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");
        }

        public async void DownloadSpinnerDocente()
        {
            string id = Application.Current.Properties["id_utente"].ToString();
            string url = URL + "id_studente=" + id;

            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, Docente> res = JsonConvert.DeserializeObject<Dictionary<string, Docente>>(result);
                List<Docente> docenti = new List<Docente>();

                foreach (KeyValuePair<string, Docente> elem in res)
                {

                    docenti.Add(new Docente(elem.Value.id_professore, elem.Value.nome, elem.Value.cognome));
                }
                ((StudentBookSlotPage)mainPage).fillSpinnerDocente(docenti);
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");



        }

        public async void DownloadSlot()
        {
          

            var response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<Slot> slots = new List<Slot>();

                if (!result.Equals("[]"))
                {

                    Dictionary<string, Slot> res = JsonConvert.DeserializeObject<Dictionary<string, Slot>>(result);
                    

                    foreach (KeyValuePair<string, Slot> elem in res)
                    {

                        slots.Add(new Slot(elem.Value.id_professore, elem.Value.id_ricevimento, elem.Value.giorno, elem.Value.inizio, elem.Value.fine));
                    }

                }
               
                ((StudentBookSlotPage)mainPage).fillListSlots(slots);
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");



        }


        public async void DownloadSpinnerCorso(bool flag)
        {
           

            var response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, Corso > res = JsonConvert.DeserializeObject<Dictionary<string, Corso>>(result);
                List<Corso> corsi = new List<Corso>();

                foreach (KeyValuePair<string, Corso> elem in res)
                {

                    corsi.Add(new Corso(elem.Value.id_corso, elem.Value.nome));
                }
                if (flag)
                {
                    ((StudentBookSlotPage)mainPage).fillSpinnerCorso(corsi);
                }
                else
                {
                    ((StudentNewEventPage)mainPage).fillSpinnerCorso(corsi);
                }

            }
            else
                Debug.WriteLine("Nothing retrieved from server.");
            
        }

        public async void BookSlot(string id_professore)
        {
            var response = await _client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result.ToString();
                string res = JsonConvert.DeserializeObject<string>(result);
                switch (res)
                {
                    case "-1":
                        DependencyService.Get<IMessage>().ShortAlert("Prenotazione fallita: lo slot è stato già prenotato");
                        break;
                    case "-2":
                        DependencyService.Get<IMessage>().ShortAlert("Prenotazione fallita: riprova");
                        break;
                    case "-3":
                        DependencyService.Get<IMessage>().ShortAlert("Prenotazione fallita: Hai già prenotato un ricevimento con questo docente in questo giorno");
                        break;
                    default:
                        DependencyService.Get<IMessage>().ShortAlert("Prenotazione riuscita: lo slot è stato prenotato con successo!");

                        break;

                }
            }
            else
            {
                Debug.WriteLine("Nothing retrieved from server.");
            }

            URL = "http://pmapp.altervista.org/ricevimenti_liberi.php?id_professore=" + id_professore;
            DownloadSlot();
        }

       




    }



   



}
