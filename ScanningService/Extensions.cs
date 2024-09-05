using Ayri.ScanningService.Models;
using WIA;
using static Ayri.ScanningService.ScanningService;

namespace Ayri.ScanningService;

public static class Extensions {


    /// <summary>
    /// Sets properties values in the given Item object.
    /// </summary>
    internal static void SetProperties(this Item item, ScanProperties properties) {
        SetWIAProperty(item, WIA_SCAN_COLOR_MODE, properties.ColorMode);
        SetWIAProperty(item, WIA_SCAN_BITS_PER_PIXEL, properties.BitsPerPixel);
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, properties.Resolution);
        SetWIAProperty(item, WIA_VERTICAL_SCAN_RESOLUTION_DPI, properties.Resolution);
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_START_PIXEL, MilimetersToPixels(properties.HorizontalStart, properties.Resolution));
        SetWIAProperty(item, WIA_VERTICAL_SCAN_START_PIXEL, MilimetersToPixels(properties.VerticalStart, properties.Resolution));
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, MilimetersToPixels(properties.HorizontalSize, properties.Resolution));
        SetWIAProperty(item, WIA_VERTICAL_SCAN_SIZE_PIXELS, MilimetersToPixels(properties.VerticalSize, properties.Resolution));
        SetWIAProperty(item, WIA_SCAN_BRIGHTNESS_PERCENTS, properties.Brightness);
        SetWIAProperty(item, WIA_SCAN_CONTRAST_PERCENTS, properties.Contrast);
    }


    /// <summary>
    /// Sets horizontal and vertical sizes for a given page size.
    /// </summary>
    public static ScanProperties SetPageSize(this ScanProperties properties, PageSize pageSize) {
        switch (pageSize) {
            case PageSize.A4:
                properties.HorizontalSize = A4_SIZE_HORIZONTAL;
                properties.VerticalSize = A4_SIZE_VERTICAL;
                break;
            case PageSize.A5:
                properties.HorizontalSize = A5_SIZE_HORIZONTAL;
                properties.VerticalSize = A5_SIZE_VERTICAL;
                break;
            case PageSize.A5H:
                properties.HorizontalSize = A5H_SIZE_HORIZONTAL;
                properties.VerticalSize = A5H_SIZE_VERTICAL;
                break;
            case PageSize.A6:
                properties.HorizontalSize = A6_SIZE_HORIZONTAL;
                properties.VerticalSize = A6_SIZE_VERTICAL;
                break;
            case PageSize.A6H:
                properties.HorizontalSize = A6H_SIZE_HORIZONTAL;
                properties.VerticalSize = A6H_SIZE_VERTICAL;
                break;
        }
        return properties;
    }
}
