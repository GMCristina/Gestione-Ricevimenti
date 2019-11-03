using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gestione_Ricevimenti.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]

namespace Gestione_Ricevimenti.Droid
{
    class MessageAndroid : IMessage
    {
        public void ShortAlert(string msg)
        {
            Toast.MakeText(Application.Context, msg , ToastLength.Short).Show();
        }
    }
}