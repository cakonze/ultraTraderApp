﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using ultraTrader.Models;

namespace ultraTrader.Droid
{
    [Activity(Label = "UltraTrader - Recomendações", Icon = "@drawable/ut_icon", Theme = "@style/MainTheme", MainLauncher = false, NoHistory = true)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            FirebaseApp.InitializeApp(Application.Context);

            LoadApplication(new App());                       
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}