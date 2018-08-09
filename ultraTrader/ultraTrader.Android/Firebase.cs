using System;
using System.Threading.Tasks;
using Firebase.Auth;

using ultraTrader.Services;
using ultraTrader.Models;
using Xamarin.Forms;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics;
using System.Text;

[assembly: Dependency(typeof(ultraTrader.Android.FirebaseDroid))]
namespace ultraTrader.Android
{
    public class FirebaseDroid : IFirebase
    {       
        public async Task<IUsuario> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                IAuthResult user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                GetTokenResult tokenResult = await user.User.GetIdTokenAsync(false);
                Usuario usr = new Usuario();
                usr.Uid = user.User.Uid;
                usr.Token = tokenResult.Token;
                usr.Nome = user.User.DisplayName;
                usr.Email = email;
                usr.DtLastLogin = DateTime.Now;

                return usr;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IUsuario> RegisterUser(string email, string password, string nome)
        {
            try
            {
                IAuthResult result = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);

                await result.User.UpdateProfileAsync(new UserProfileChangeRequest.Builder().SetDisplayName(nome).Build());
                GetTokenResult tokenResult = await result.User.GetIdTokenAsync(false);

                return new Usuario() { Uid = result.User.Uid, Token = tokenResult.Token, Email = email, DtLastLogin = DateTime.Now, Nome = nome, IsAdmin = false, IsPro = false };
            }
            catch (FirebaseAuthUserCollisionException e)
            {
                throw new Exception("Já existe um usuário registrado com o e-mail informado: " + email);
            }
            catch (FirebaseAuthEmailException e)
            {
                throw new Exception("O e-mail informado está preenchido incorretamente: " + email);
            }
            catch (Exception e)
            {
                throw new Exception("Erro: " + e.Message);
            }
        }

        public async Task ResetPassword(string email)
        {
            await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);

        }

        //public IUsuario GetUserInfo()
        //{
        //    try
        //    {
        //        FirebaseUser fbUser = FirebaseAuth.Instance.CurrentUser;
        //        GetTokenResult tokenResult = null;
        //        Command cmd = new Command(async () => { tokenResult = await fbUser.GetIdTokenAsync(true); });

        //        Usuario retUser = new Usuario
        //        {
        //            Nome = fbUser.DisplayName,
        //            Email = fbUser.Email,
        //            Uid = fbUser.Uid,
        //            Token = tokenResult.Token,
        //            DtLastLogin = DateTime.Now

        //            // TODO: recuperar os outros campos das outras tabelas para o Usuário

        //        };
        //        return retUser;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

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


        //public async void UpdateUser(string Uid, string nome, bool isAdmin, bool isPro)
        //{
        //    await dbRoot.Child("usuarios").Child(Uid).Child("nome").SetValueAsync(nome);
        //    await dbRoot.Child("usuarios").Child(Uid).Child("IsAdmin").SetValueAsync(isAdmin);
        //    await dbRoot.Child("usuarios").Child(Uid).Child("IsPro").SetValueAsync(isPro);
        //}

    }

    public class Usuario : IUsuario
    {
        public string Uid { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsPro { get; set; }

        public DateTime DtLastLogin { get; set; }

        public string Token { get; set; }
        public string Senha { get; set; }
    }
}
