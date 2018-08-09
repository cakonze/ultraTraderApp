using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ultraTrader.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ultraTrader.Services
{
    public class DbFirebase
    {
        public FirebaseClient client; // = new FirebaseClient("https://ultratrader-210414.firebaseio.com/");
        //public FirebaseAuth userAuth;

        public DbFirebase()
        {
            client = new FirebaseClient("https://ultratrader-210414.firebaseio.com/");
            //  userAuth = new FirebaseAuth();
        }

        public async Task<bool> UpsertUsuario(IUsuario user)
        {
            var usuario = client.Child("usuarios").Child(user.Uid).WithAuth(Sessao.Usuario.Token);
            await usuario.PutAsync<IUsuario>(user);

            return true;
        }

        public async Task<IUsuario> GetUsuario(string Uid)
        {
            IUsuario user = null;

            var usuario = client.Child("usuarios").Child(Uid).WithAuth(Sessao.Usuario.Token);
            user = await usuario.OnceSingleAsync<IUsuario>();

            return user;
        }
    }

    public interface IFirebase
    {
        Task<IUsuario> LoginWithEmailPassword(string email, string password);

        Task<IUsuario> RegisterUser(string email, string password, string nome);

        Task ResetPassword(string email);

    }
}
