
using static Ayri.ScanningService.ScanningService;

namespace Ayri.ScanningService.Models;

/// <summary>
/// This class has the scan properties a device use for aquire an image.
/// </summary>
public class ScanProperties {

    /// <summary>
    /// Sets or gets the image format for the scanned image.<br/>
    /// Initial value: JPEG
    /// </summary>
    public string? ImageFormatId { get; set; } = FORMAT_ID_JPEG;

    /// <summary>
    /// Posible values: 0=Unspecified; 1=Color; 2=Grayscale; 4=Black and white text.<br/>
    /// Initial value: 1
    /// </summary>
    public int ColorMode { get; set; } = COLOR_MODE_COLOR;

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
    /// Initial value: 297 (A4 layout)
    /// </summary>
    public int HorizontalSize { get; set; } = A4_SIZE_HORIZONTAL;

    /// <summary>
    /// Value in milimeters.<br/>
    /// Initial value: 210 (A4 layout)
    /// </summary>
    public int VerticalSize { get; set; } = A4_SIZE_VERTICAL;


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
