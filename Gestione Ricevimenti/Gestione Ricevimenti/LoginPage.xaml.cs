﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Essentials;

namespace Gestione_Ricevimenti
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {

            InitializeComponent();

        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            if (username.Text != null && password.Text != null)
            {
                if (CheckConnection.CheckInternetConnection(this)) {
                    ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/login.php");
                    request.LoginRequest(username.Text, password.Text);
                }
                
            }
        }

        public void SaveUtente (UtenteResp u)
        {
            Application.Current.Properties["id_utente"] = u.id;
            Application.Current.Properties["tipo_utente"] = u.tipo;
            Application.Current.Properties["matricola"] = username.Text;
            Application.Current.Properties["password"] = password.Text;
            Application.Current.SavePropertiesAsync();

            switch (Application.Current.Properties["tipo_utente"].ToString())
            {
                case "s":
                    // Navigation.PushModalAsync(new StudentHomePage());
                    var c = Navigation.NavigationStack.Count();

                    for (int i = 0; i < c - 1; i++)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[i]);

                    }

                    Navigation.PushAsync(new StudentHomePage());
                    Navigation.RemovePage(this);

                    break;
                case "p":
                    Navigation.PushAsync(new ProfHomePage());
                    Navigation.RemovePage(this);
                    break;
                
           

            }
        }

    }
}
