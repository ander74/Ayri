using System.Globalization;

namespace Ayri.Core.Extensions;

public static class DecimalExtensions {


    /// <summary>
    /// Return the decimal value as string in 0.00 format as default.
    /// </summary>
    /// <param name="decimales">Number of decimal positions.</param>
    /// <param name="noZero">If true and decimal value is 0, returns an empty string.</param>
    /// <returns>Decimal value in 0.00 format.</returns>
    public static string ToText(this decimal numero, int decimales = 2, bool noZero = false) {
        if (noZero && numero == 0m) return "";
        var texto = numero.ToString($"0.{new string('0', decimales)}", CultureInfo.InvariantCulture).Replace(".", ",");
        return texto;
    }


    /// <summary>
    /// Returns a TimeSpan with the decimal value as decimal hours.
    /// </summary>
    /// <returns>TimeSpan with decimal value as decimal hours</returns>
    public static TimeSpan ToTimeSpan(this decimal numero) {
        int horas = Convert.ToInt32(Math.Truncate(numero));
        int minutos = Convert.ToInt32(Math.Truncate((numero * 60) % 60));
        return new TimeSpan(horas, minutos, 0);
    }


    /// <summary>
    /// Rounds a decimal to two decimal positions.
    /// </summary>
    /// <returns>A decimal with two decimal positions.</returns>
    public static decimal To2Decimals(this decimal numero) => Math.Round(numero, 2);


}
