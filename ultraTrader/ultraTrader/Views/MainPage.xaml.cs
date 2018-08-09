using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.Models;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace ultraTrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MenuPage menuPage;
        List<KeyValuePair<string, Page>> liPaginas = new List<KeyValuePair<string, Page>>();

        public MainPage()
        {
            InitializeComponent();          

            menuPage = new MenuPage();
            menuPage._MainPage = this;

            liPaginas.Add(new KeyValuePair<string, Page>("Meu Plano", new MeuPlanoPage()));
            liPaginas.Add(new KeyValuePair<string, Page>("Recomendações", new RecomendacoesPage()));
            liPaginas.Add(new KeyValuePair<string, Page>("Carteira da semana", new CarteiraPage()));
            liPaginas.Add(new KeyValuePair<string, Page>("Performance", new PerformancePage()));
            liPaginas.Add(new KeyValuePair<string, Page>("Chat", new ChatPage()));
            liPaginas.Add(new KeyValuePair<string, Page>("Configurações", new ConfiguracoesPage()));

            this.Master = menuPage; 
            this.Detail = new NavigationPage(new RecomendacoesPage());

          
        }

        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Logout", "Deseja encerrar esta sessão?", "Sim", "Não"))
                {
                    Preferences.Set("Logado", false);
                    Preferences.Set("Uid", "");
                    App.Current.MainPage = new NavigationPage(new LoginPage());

                    base.OnBackButtonPressed();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }

        public Page GetPageMenu(string nome)
        {
            foreach (KeyValuePair<string, Page> kp in liPaginas)
                if (kp.Key.ToLower() == nome.ToLower())
                    return kp.Value;

            return new Page();
        }

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as ItemMenu;
        //    if (item != null)
        //    {
        //        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        //        menuPage.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }

        //}
    }
}