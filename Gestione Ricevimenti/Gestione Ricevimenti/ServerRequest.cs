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
                Dictionary<string, RicevimentoHomePageResp> result_event = JsonConvert.DeserializeObject<Dictionary<string, RicevimentoHomePageResp>>(result);
                List<RicevimentoHomePageResp> events = new List<RicevimentoHomePageResp>();

                foreach (KeyValuePair<string, RicevimentoHomePageResp> elem in result_event)
                {
                 
                    events.Add(new RicevimentoHomePageResp(elem.Value.id_ricevimento, elem.Value.nome, elem.Value.cognome, elem.Value.giorno, elem.Value.inizio, elem.Value.fine, elem.Value.corso, elem.Value.stato));
                }
                ((StudentHomePage)mainPage).fillListStudentHomePage(events);
            }
            else
                Debug.WriteLine("Nothing retrieved from server.");


            
        }
            

             
    }
}
