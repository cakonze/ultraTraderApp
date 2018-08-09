using Newtonsoft.Json;
using System;
using System.Text;
using ultraTrader.Services;

namespace ultraTrader.Models
{
    public interface IUsuario
    {
        string Uid { get; set; }

        string Email { get; set; }

        string Nome { get; set; }

        bool IsAdmin { get; set; }

        bool IsPro { get; set; }

        DateTime DtLastLogin { get; set; }

        string Token { get; set; }
        string Senha { get; set; }
    }
}
