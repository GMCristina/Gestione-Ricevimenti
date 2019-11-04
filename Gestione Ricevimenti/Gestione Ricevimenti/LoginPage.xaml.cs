using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Gestione_Ricevimenti
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            if (Application.Current.Properties.ContainsKey("id_utente"))
            {
               
                switch (Application.Current.Properties["tipo_utente"].ToString())
                {
                    case "s":
                       //Navigation.InsertPageBefore(new StudentHomePage(), this);
                       // Navigation.PopAsync();
                        Navigation.PushAsync(new StudentHomePage());
                        break;
                    case "p":
                        Navigation.PushAsync(new ProfHomePage());
                        break;
                }

            }
            else
            {
                InitializeComponent();
            }
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            if (username.Text != null && password.Text != null)
            {
                ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/login.php");
                request.LoginRequest(username.Text, password.Text);
            }
        }

        public void SaveUtente (UtenteResp u)
        {
            Application.Current.Properties["id_utente"] = u.id;
            Application.Current.Properties["tipo_utente"] = u.tipo;
            Application.Current.Properties["matricola"] = username.Text;
            Application.Current.Properties["password"] = password.Text;
            // Debug.WriteLine(Application.Current.Properties["id_utente"].ToString() + "/" + Application.Current.Properties["tipo_utente"].ToString() + "/" + Application.Current.Properties["matricola"].ToString() + "/" + Application.Current.Properties["password"].ToString());
            switch (Application.Current.Properties["tipo_utente"].ToString())
            {
                case "s":
                    Navigation.PushAsync(new StudentHomePage());
                   // Navigation.RemovePage(this);
                    break;
                case "p":
                    Navigation.PushAsync(new ProfHomePage());
                    break;
                
           

            }
        }
    }
}
