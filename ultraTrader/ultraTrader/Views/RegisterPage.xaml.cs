using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.Services;
using ultraTrader.ViewModels;
using ultraTrader.Models;
using System.Collections.Generic;

namespace ultraTrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterViewModel viewModel;

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RegisterViewModel();

            txtEmail.Text = "email@docara.com";
            txtSenha.Text = "senha123";
            txtSenhaConfirmacao.Text = "senha123";
            txtNome.Text = "Fulano de Tal Novamente";
        }

        async void OnBtnVoltarClicked(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                return;

            await Navigation.PopModalAsync();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            await RegistrarConta();
        }

        private async Task RegistrarConta()
        {
            if (viewModel.IsBusy)
                return;

            #region Validações
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                Util.ExibeMensagem("Informe seu nome!");
                txtNome.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                Util.ExibeMensagem("Preencha o email a ser utilizado!");
                txtEmail.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtSenha.Text))
            {
                Util.ExibeMensagem("Informe uma senha para utilizar na sua conta!");
                txtSenha.Focus();
                return;
            }

            if (txtSenha.Text.Length <= 5)
            {
                Util.ExibeMensagem("Informe uma senha com 6 caracteres ou mais!");
                txtSenha.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtSenhaConfirmacao.Text))
            {
                Util.ExibeMensagem("Confirme a senha digitada!");
                txtSenhaConfirmacao.Focus();
                return;
            }

            else if (txtSenha.Text.Trim() != txtSenhaConfirmacao.Text.Trim())
            {
                Util.ExibeMensagem("As senhas informadas estão diferentes!");
                txtSenhaConfirmacao.Focus();
                return;
            }

            #endregion

            viewModel.IsBusy = true;
            try
            {
                var newUser = await viewModel.Firebase.RegisterUser(txtEmail.Text, txtSenha.Text, txtNome.Text);
                if (!String.IsNullOrEmpty(newUser.Uid))
                {
                    await Navigation.PopModalAsync();
                    MessagingCenter.Send<RegisterPage, List<string>>(this, "finalizaRegistroDeConta", new List<string>() { txtEmail.Text, txtSenha.Text });
                }
                else Util.ExibeMensagem("Não foi possível registrar a conta!");
            }
            catch (Exception ex)
            {
                Util.ExibeMensagem(ex.Message);
            }
            viewModel.IsBusy = false;
        }
    }
}