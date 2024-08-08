namespace Ayri.Core;

public static class Texts {


    /// <summary>
    /// Full month names in spanish.
    /// </summary>
    public static string[] Meses = { "Desconocido", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };


    /// <summary>
    /// Abreviated month names in spanish.
    /// </summary>
    public static string[] MesesAbr = { "Des", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };


    /// <summary>
    /// Full dayweek name in spanish.
    /// </summary>
    public static string[] DiasSemana = { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };


    /// <summary>
    /// Abreviated dayweek name in spanish.
    /// </summary>
    public static string[] DiasSemanaAbr = { "Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };


    /// <summary>
    /// Returns the full name or abreviated name of given day week in spanish.
    /// </summary>
    public static string GetDiaSemana(DayOfWeek dia, bool isAbreviated = false) => dia switch {
        DayOfWeek.Sunday => isAbreviated ? "Dom" : "Domingo",
        DayOfWeek.Monday => isAbreviated ? "Lun" : "Lunes",
        DayOfWeek.Tuesday => isAbreviated ? "Mar" : "Martes",
        DayOfWeek.Wednesday => isAbreviated ? "Mié" : "Miércoles",
        DayOfWeek.Thursday => isAbreviated ? "Jue" : "Jueves",
        DayOfWeek.Friday => isAbreviated ? "Vie" : "Viernes",
        DayOfWeek.Saturday => isAbreviated ? "Sáb" : "Sábado",
        _ => isAbreviated ? "Des" : "Desconocido",
    };


}
