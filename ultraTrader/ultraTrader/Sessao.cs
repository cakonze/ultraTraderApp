using System;
using System.Collections.Generic;
using System.Text;
using ultraTrader.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

using ultraTrader.Models;
using Xamarin.Essentials;

namespace ultraTrader
{
    public static class Sessao
    {
        public static IUsuario Usuario { get; set; }

        public static void Start(IUsuario usuario)
        {
            Usuario = usuario;
            DbFirebase fb = new DbFirebase();
            fb.UpsertUsuario(usuario);

        }

        public static async Task Restart()
        {
            DbFirebase fb = new DbFirebase();
            Usuario = await fb.GetUsuario(Preferences.Get("Uid", ""));
        }
    }
}
