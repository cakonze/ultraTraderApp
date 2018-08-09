using Android.App;
using Android.Widget;
using ultraTrader.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ultraTrader.Android.MessageAndroid))]
namespace ultraTrader.Android
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}