using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.Models;
using System.Diagnostics;
using Xamarin.Essentials;
using ultraTrader.ViewModels;

namespace ultraTrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage 
    {
        public MainPage _MainPage { set; get; }
        private List<Label> liMenuLabels = new List<Label>();
        MenuViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MenuViewModel();

            lblNomeUsuario.Text = Sessao.Usuario.Nome;
            lblEmailUsuario.Text = Sessao.Usuario.Email;

            foreach (View lbl in gridMenu.Children)
                if (lbl.GetType() == typeof(Label))
                    liMenuLabels.Add((Label)lbl);

            #region gesture events 

            lblMeuPlano.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblMeuPlano);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Meu plano")); 
                    _MainPage.IsPresented = false;
                })
            });

            lblRecomendacoes.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblRecomendacoes);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Recomendações"));
                    _MainPage.IsPresented = false;
                })
            });

            lblCarteira.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblCarteira);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Carteira da semana")); 
                    _MainPage.IsPresented = false;
                })
            });

            lblPerformance.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblPerformance);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Performance"));
                    _MainPage.IsPresented = false;
                })
            });

            lblChat.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblChat);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Chat"));
                    _MainPage.IsPresented = false;
                })
            });

            lblConfiguracoes.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblConfiguracoes);
                    _MainPage.Detail = new NavigationPage(_MainPage.GetPageMenu("Configurações"));
                    _MainPage.IsPresented = false;
                })
            });


            lblLogout.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    trocaCorItemMenu(lblLogout);
                    Preferences.Set("Logado", false);
                    Sessao.Usuario = null;

               //     Preferences.Set("Uid", "");
                //    Preferences.Set("Token", "");
                    App.Current.MainPage = new NavigationPage(new LoginPage());                   
                })
            });

            #endregion
        }

        private void trocaCorItemMenu(Label label)
        {
            foreach (Label lbl in liMenuLabels)
            {
                lbl.FontAttributes = label == lbl ? FontAttributes.Bold : FontAttributes.None;
                lbl.TextColor = label == lbl ? Color.Red : Color.Black;
            }
        }
    }
}