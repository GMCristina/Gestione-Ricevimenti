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
    }
}
