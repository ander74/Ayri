namespace Ayri.ScanningService.Models;

/// <summary>
/// This class represents an scanning device in this service.
/// </summary>
public class Scanner {

    /// <summary>
    /// Name of the scanner.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Id of the scanner.
    /// </summary>
    public string DeviceId { get; set; } = string.Empty;

}
