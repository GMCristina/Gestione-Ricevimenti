using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Gestione_Ricevimenti
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (!Application.Current.Properties.ContainsKey("id_utente"))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                switch (Application.Current.Properties["tipo_utente"].ToString())
                {
                    case "s":
                    
                        MainPage = new NavigationPage(new StudentHomePage());
                        break;
                    case "p":
                        MainPage = new NavigationPage ( new ProfHomePage());
                        break;
                }
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
