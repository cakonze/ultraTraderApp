using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.Views;
using ultraTrader.Services;
using Xamarin.Essentials;
using System.Diagnostics;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ultraTrader
{
    public partial class App : Application
    {
        const string CONFIG_KEY = "AIzaSyAbPaFekoB-cGEn1jZRow6Uj10r7kls3V8";
        public static FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(CONFIG_KEY));

        public App()
        {
            InitializeComponent();
                   
            if (Preferences.Get("Logado", false))
            {
                try
                {
                    Sessao.Restart();
                    MainPage = new MainPage();
                }
                catch (Exception ex)
                {
                    Util.ExibeMensagem("Não foi possível logar com sua conta: " + ex.Message);
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
                MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            //Util.ExibeMensagem("OnStart:" + Preferences.Get("Uid", ""));
        }

        protected override void OnSleep()
        {
            //Util.ExibeMensagem("OnSleep:" + Preferences.Get("Uid", ""));
        }

        protected override void OnResume()
        {
            //Util.ExibeMensagem("OnResume:" + Preferences.Get("Uid", ""));
        }
    }    
}
