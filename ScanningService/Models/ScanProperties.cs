namespace ScanningService.Models;

/// <summary>
/// This class has the scan properties a device use for aquire an image.
/// </summary>
public class ScanProperties {

    /// <summary>
    /// Posible values: 0=Unspecified; 1=Color; 2=Grayscale; 4=Black and white text.<br/>
    /// Initial value: 1
    /// </summary>
    public int ColorMode { get; set; } = 1;

    /// <summary>
    /// Leave in 24.<br/>
    /// Initial value: 24
    /// </summary>
    public int BitsPerPixel { get; set; } = 24;

    /// <summary>
    /// Posible values: 100, 150, 200, 300, 600.<br/>
    /// Initial value: 200
    /// </summary>
    public int Resolution { get; set; } = 200;

    /// <summary>
    /// Value in milimeters.<br/>
    /// Initial value: 0
    /// </summary>
    public int HorizontalStart { get; set; } = 0;

    /// <summary>
    /// Value in milimeters.<br/>
    /// Initial value: 0
    /// </summary>
    public int VerticalStart { get; set; } = 0;

    /// <summary>
    /// Value in milimeters.<br/>
    /// Initial value: 210 (A4 layout)
    /// </summary>
    public int HorizontalSize { get; set; } = 210;

    /// <summary>
    /// Value in milimeters.<br/>
    /// Initial value: 297 (A4 layout)
    /// </summary>
    public int VerticalSize { get; set; } = 297;

    /// <summary>
    /// Value between -1000 and 1000.<br/>
    /// Initial value: 0
    /// </summary>
    public int Brightness { get; set; } = 0;

    /// <summary>
    /// Value between -1000 and 1000.<br/>
    /// Initial value: 0
    /// </summary>
    public int Contrast { get; set; } = 0;

}
