using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.ViewModels;
using ultraTrader.Services;
using System;
using Xamarin.Essentials;

using ultraTrader.Models;
using System.Collections.Generic;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;

namespace ultraTrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new LoginViewModel();

            lblEsqueciSenha.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (!validarCampos())
                        return;

                    viewModel.IsBusy = true;
                    try
                    {
                        await viewModel.Firebase.ResetPassword(txtEmail.Text.Trim());
                        Util.ExibeMensagem("Foi enviado um e-mail para " + txtEmail.Text + " contendo instruções para resetar sua senha.", false);
                    }
                    catch (Exception ex)
                    {
                        Util.ExibeMensagem(ex.Message);
                    }
                    viewModel.IsBusy = false;
                })
            });

            lblNovaConta.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (viewModel.IsBusy)
                        return;

                    MessagingCenter.Subscribe<RegisterPage, List<string>>(this, "finalizaRegistroDeConta", (pagOrigem, dadosLogin) =>
                    {
                        txtEmail.Text = dadosLogin[0]; //.Email;
                        txtSenha.Text = dadosLogin[1]; //.Senha;

                        Util.ExibeMensagem("Sua conta gratuita foi criada com sucesso!", false);
                    });

                    viewModel.IsBusy = true;
                    await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
                    viewModel.IsBusy = false;
                })
            });

            chkSalvarCredenciais.IsToggled = Preferences.Get("salvar_credenciais", false);
            txtEmail.Text = Preferences.Get("salvar_credenciais", false) ? Preferences.Get("email", "") : "";
            txtSenha.Text = Preferences.Get("salvar_credenciais", false) ? Preferences.Get("senha", "") : "";
        }

        private bool validarCampos()
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                Util.ExibeMensagem("Informe seu e-mail!");
                txtEmail.Focus();
                return false;
            }

            if (!Util.ValidarEmail(txtEmail.Text))
            {
                Util.ExibeMensagem("O e-mail informado é inválido!");
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (!validarCampos())
                return;

            try
            {
                viewModel.IsBusy = true;

                var usuario = await viewModel.Firebase.LoginWithEmailPassword(txtEmail.Text, txtSenha.Text);

                if (!String.IsNullOrEmpty(usuario.Token))
                {
                    Preferences.Set("Uid", usuario.Uid);
                    Preferences.Set("Token", usuario.Token);
                    Preferences.Set("Logado", true);
                    Sessao.Start(usuario);

                    if (chkSalvarCredenciais.IsToggled)
                    {
                        Preferences.Set("salvar_credenciais", chkSalvarCredenciais.IsToggled);
                        Preferences.Set("email", txtEmail.Text);
                        Preferences.Set("senha", txtSenha.Text);
                    } else
                    {
                        Preferences.Remove("salvar_credenciais");
                        Preferences.Remove("email");
                        Preferences.Remove("senha");

                        txtEmail.Text = "";
                        txtSenha.Text = "";
                    }

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                {
                    Preferences.Remove("Uid");
                    Preferences.Remove("Token");
                    Preferences.Set("Logado", false);
                    Util.ExibeMensagem("Não foi possível recuperar o Uid.");
                }

            } catch (Exception ex)
            {
                Preferences.Remove("Uid");
                Preferences.Remove("Token");
                Preferences.Set("Logado", false);
                Util.ExibeMensagem(ex.Message);
            }
            viewModel.IsBusy = false;
        }
    }
}