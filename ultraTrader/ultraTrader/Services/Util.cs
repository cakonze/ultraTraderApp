using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ultraTrader.Services
{
    public static class Util
    {
        public static void ExibeMensagem(string mensagem)
        {
            ExibeMensagem(mensagem, true);
        }

        public static void ExibeMensagem(string mensagem, bool boCurta)
        {
            if (boCurta)
                DependencyService.Get<IMessage>().ShortAlert(mensagem);
            else
                DependencyService.Get<IMessage>().LongAlert(mensagem);
        }

        const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public static bool ValidarEmail(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(value);
        }

    }


}
