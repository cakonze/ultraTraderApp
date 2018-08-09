﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ultraTrader.Models;
using ultraTrader.ViewModels;
using Xamarin.Essentials;

namespace ultraTrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("Logado", false);
            Preferences.Set("Uid", "");
 
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}