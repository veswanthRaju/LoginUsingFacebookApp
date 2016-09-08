using Android.App;
using Android.OS;
using Android.Widget;
using System.Json;
using System.Threading.Tasks;
using Xamarin.Auth;
using System;
using Android.Content;

namespace SampleFbLoginApp
{
    [Activity(Label = "SampleFbLoginApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Intent activity2;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var facebook = FindViewById<Button>(Resource.Id.FacebookButton);
            facebook.Click += delegate { LoginToFacebook(true); };

            activity2 = new Intent(this, typeof(Userdata));
        }

        private static readonly TaskScheduler UIScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        void LoginToFacebook(bool allowCancel)
        {
            var auth = new OAuth2Authenticator(
                         //Below is Facebook App id which we created on fb developer
                         clientId: Constants.ID,
                         scope: "", //By Default it is public..
                         authorizeUrl: new Uri(Constants.authorize),
                         redirectUrl: new Uri(Constants.reDirect));
            //We should give redirectUrl in redirect URl option in products in facebook-developers options

            auth.AllowCancel = allowCancel;
            
            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.Completed += (s, ee) => {
                var progressDialog = ProgressDialog.Show(this, Constants.wait, Constants.info, true);

                if (!ee.IsAuthenticated)
                {
                    var builder = new AlertDialog.Builder(this);
                    builder.SetMessage("Not Authenticated");
                    builder.SetPositiveButton("Ok", (o, e) => { });
                    builder.Create().Show();
                    progressDialog.Hide();
                    return;
                }
                
                // Now that we're logged in, make a OAuth2 request to get the user's info.
                var request = new OAuth2Request("GET", new Uri(Constants.Url), null, ee.Account);
                request.GetResponseAsync().ContinueWith(t => {
                    var builder = new AlertDialog.Builder(this);
                    progressDialog.Hide();
                    if (t.IsFaulted)
                    {
                        builder.SetTitle("Error");
                        builder.SetMessage(t.Exception.Flatten().InnerException.ToString());
                        builder.SetPositiveButton("Ok", (o, e) => { });
                        builder.Create().Show();
                    }
                    else if (t.IsCanceled)
                    {
                        builder.SetTitle("Task Canceled");
                        builder.SetPositiveButton("Ok", (o, e) => { });
                        builder.Create().Show();
                    }   
                    else
                    {
                        var obj = JsonValue.Parse(t.Result.GetResponseText());
                        
                        DataClass.userData = obj;

                        //Passing username to next Activity..
                        //activity2.PutExtra("MyData","User Details: " +  obj);
                        StartActivity(activity2);
                    }

                }, UIScheduler);
            };

            var intent = auth.GetUI(this);
            StartActivity(intent);
        }
    }
}