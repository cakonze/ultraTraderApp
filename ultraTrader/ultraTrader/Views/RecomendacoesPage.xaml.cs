using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ultraTrader.Models;
using System.Collections.ObjectModel;

namespace ultraTrader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecomendacoesPage : ContentPage
	{
		public RecomendacoesPage ()
		{
			InitializeComponent ();

            ObservableCollection<Recomendacao> recomendacoes = new ObservableCollection<Recomendacao>();
            recomendacoes.Add(new Recomendacao { IsPro = false, Ticker = "BBAS3", TipoOrdem = "Compra", ValorEntrada = Convert.ToDecimal("22.40") });

            lvRecomendacoes.ItemsSource = recomendacoes;

        }
    }
}