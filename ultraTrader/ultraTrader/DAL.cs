using System;
using System.Collections.Generic;
using System.Text;
using FirebaseSharp.Portable;
using Xamarin.Essentials;
using Newtonsoft.Json;

using ultraTrader.Models;
using ultraTrader.Services;

namespace ultraTrader
{
    class DAL
    {
        public static void UpsertUser(string strUid, string strNome, string strEmail)
        {
            // https://github.com/bubbafat/FirebaseSharp

            using (FirebaseApp app = new FirebaseApp(new Uri("https://ultratrader-210414.firebaseio.com/"), Preferences.Get("Token", "")))
            {
                app.Child("usuarios").Child(strUid).Set(JsonConvert.SerializeObject(new Usuario
                {
                    Uid = strUid,
                    Nome = strNome,
                    Email = strEmail,
                    IsAdmin = true,
                    IsPro = false,
                    Senha = "essanõavai",
                    DtLastLogin = DateTime.Now
                }), new FirebaseSharp.Portable.Interfaces.FirebaseStatusCallback((erro) =>
                {
                    Util.ExibeMensagem(erro.Code);
                }));
            }
        }

        //public async void TesteUser(Usuario user)
        //{
        //    if (dbRoot == null)
        //        dbRoot = FirebaseDatabase.Instance.Reference;

        //    FirebaseUser user = FirebaseAuth.Instance.CurrentUser;

        //    DatabaseReference users = dbRoot.Child("usuarios").Child(user.Uid);
        //    Usuario u = new Usuario
        //    {
        //        Uid = user.Uid,
        //        Nome = user.DisplayName,
        //        Email = user.Email,
        //        IsAdmin = true,
        //        IsPro = false,
        //        Senha = "essanõavai",
        //        DtLastLogin = DateTime.Now
        //    };

        //    string value = JsonConvert.SerializeObject(u, Formatting.Indented);



        //    try
        //    {
        //        await users.SetValueAsync(value);
        //    }
        //    catch (DatabaseException ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Erro: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Erro: " + ex.Message);
        //    }
        //}
    }
}
