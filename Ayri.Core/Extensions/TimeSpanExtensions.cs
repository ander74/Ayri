namespace Ayri.Core.Extensions;

public static class TimeSpanExtensions {


    /// <summary>
    /// Returns a string with TimeSpan value in hh:mm format.<br/>
    /// </summary>
    /// <param name="max24h">If true, the value can contain more than 24 hours.</param>
    /// <param name="noZero">If true, if TimeSpan value is 0, returns an empty string.</param>
    /// <returns>The TimeSpan value in hh:mm format.</returns>
    public static string ToText(this TimeSpan hora, bool max24h = false, bool noZero = false) {
        if (hora == TimeSpan.MaxValue) return "";
        if (noZero && hora.Ticks == 0) return "";
        var negativo = false;
        var horas = max24h ? hora.Hours : hora.Days * 24 + hora.Hours;
        if (horas < 0) horas = horas * -1;
        if (hora.Hours < 0 || hora.Minutes < 0) negativo = true;
        var minutos = hora.Minutes < 0 ? hora.Minutes * -1 : hora.Minutes;
        return negativo ? $"-{horas:00}:{minutos:00}" : $"{horas:00}:{minutos:00}";
    }


    /// <summary>
    /// Returns the TimeSpan value in decimal format with 6 decimal positions as default.
    /// </summary>
    /// <param name="decimales">Number of decimal positions to return.</param>
    /// <returns></returns>
    public static decimal ToDecimal(this TimeSpan hora, int decimales = 6) => Math.Round(Convert.ToDecimal(hora.TotalHours), decimales);


}
