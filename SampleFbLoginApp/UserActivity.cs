using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Transitions;
using Android.Widget;
using Java.Net;
using System;
using System.Net;

namespace SampleFbLoginApp
{
    [Activity(Label = "Userdata")]
    public class UserActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserData);

            var text = DataClass.userData;
            var imageUrl = text["picture"]["data"]["url"];

            var username = FindViewById<TextView>(Resource.Id.Username);
            var picture = FindViewById<ImageView>(Resource.Id.userImageView);
            
            username.Text = text["name"];

            var imageBitmap = GetImageBitmapFromUrl(imageUrl);
            picture.SetImageBitmap(imageBitmap);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}