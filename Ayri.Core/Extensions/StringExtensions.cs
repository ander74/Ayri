using System.Globalization;

namespace Ayri.Core.Extensions;

public static class StringExtensions {


    /// <summary>
    /// Converts a string in a TimeSpan. If string has no valid TimeSpan value, returns a TimeSpan.MaxValue.
    /// </summary>
    /// <returns>A TimeSpan with string value in it.</returns>
    public static TimeSpan ToTimeSpan(this string texto) {
        if (string.IsNullOrWhiteSpace(texto)) return TimeSpan.MaxValue;
        var textos = texto.Replace(".", ":").Replace(",", ":").Split(':');
        if (textos.Length != 2) return TimeSpan.MaxValue;
        if (int.TryParse(textos[0], out int hora) && int.TryParse(textos[1], out int minutos)) {
            if (hora < 0) minutos = minutos * -1;
            return new TimeSpan(hora, minutos, 0);
        }
        return TimeSpan.MaxValue;
    }


    /// <summary>
    /// Converts a string in a TimeSpan. If string has no valid TimeSpan value, returns a TimeSpan.Zero.
    /// </summary>
    /// <returns>A TimeSpan with string value in it.</returns>
    public static TimeSpan ToTimeSpanNoNull(this string texto) {
        if (string.IsNullOrWhiteSpace(texto)) return TimeSpan.Zero;
        var textos = texto.Replace(".", ":").Replace(",", ":").Split(':');
        if (textos.Length != 2) return TimeSpan.Zero;
        if (int.TryParse(textos[0], out int hora) && int.TryParse(textos[1], out int minutos)) {
            if (hora < 0) minutos = minutos * -1;
            return new TimeSpan(hora, minutos, 0);
        }
        return TimeSpan.Zero;
    }


    /// <summary>
    /// Converts a string in a decimal. If string has no valid decimal value, returns 0.
    /// </summary>
    /// <returns>A decimal with string value in it.</returns>
    public static decimal ToDecimal(this string texto) {
        if (string.IsNullOrWhiteSpace(texto)) return 0m;
        texto = texto.Replace(",", ".").Replace("€", "");
        if (decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numero)) {
            return numero;
        }
        return 0m;
    }


    /// <summary>
    /// Converts a string in a int. If string has no valid int value, returns 0.
    /// </summary>
    /// <returns>A int with string value in it.</returns>
    public static int ToInt(this string texto) {
        if (int.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out int numero)) {
            return numero;
        }
        return 0;
    }


}
