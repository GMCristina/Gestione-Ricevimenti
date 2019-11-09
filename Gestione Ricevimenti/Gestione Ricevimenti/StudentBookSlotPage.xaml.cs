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
	public partial class StudentBookSlotPage : ContentPage
	{
		public StudentBookSlotPage ()
		{
           
            ServerRequest request = new ServerRequest(this, "http://pmapp.altervista.org/professori.php?");
            request.DownloadSpinnerDocente();

            InitializeComponent ();
		}

        public void fillListStudentBookSlotPage(List<RicevimentoHomePage> l)
        {
            slots.ItemsSource = l;
        }

    }
}