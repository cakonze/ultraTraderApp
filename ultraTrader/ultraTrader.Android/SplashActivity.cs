using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ultraTrader.Droid
{
    [Activity(Label = "UltraTrader - Recomendações", Theme = "@style/MainTheme.Splash", Icon = "@drawable/ut_icon", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}