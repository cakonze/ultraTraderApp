using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ultraTrader.Models;
using ultraTrader.Views;
using ultraTrader.Services;

namespace ultraTrader.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command RegistrarContaCommand { get; set; }

        public RegisterViewModel()
        {
            Title = "Registrar nova conta";
        }         
    }
}