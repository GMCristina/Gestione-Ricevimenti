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

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Prova",
                 Order = ToolbarItemOrder.Primary,

            });
            
          


          
               // Navigation.RemovePage(Navigation.NavigationStack[0]);
            
        }

        public void fillListStudentHomePage(List<RicevimentoHomePageResp> l)
        {
            ricevimenti.ItemsSource = l;
        }
    }
}