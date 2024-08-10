using Ayri.ScanningService.Models;
using WIA;

namespace Ayri.ScanningService;

/// <summary>
/// This service helps in the scanning image process acting as intermediator between an application and the COM scanning component of Windows.
/// </summary>
public class ScanningService {

    const string WIA_SCAN_COLOR_MODE = "6146";
    const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
    const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
    const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
    const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
    const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
    const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
    const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
    const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
    const string WIA_SCAN_BITS_PER_PIXEL = "4104";

    public const string FORMAT_ID_BMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
    public const string FORMAT_ID_JPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
    public const string FORMAT_ID_PNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
    public const string FORMAT_ID_GIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
    public const string FORMAT_ID_TIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";

    public const int COLOR_MODE_COLOR = 1;
    public const int COLOR_MODE_GRAYSCALE = 2;
    public const int COLOR_MODE_TEXT = 4;

    public const int A4_SIZE_HORIZONTAL = 210;
    public const int A4_SIZE_VERTICAL = 297;
    public const int A5_SIZE_HORIZONTAL = 149;
    public const int A5_SIZE_VERTICAL = 210;
    public const int A5H_SIZE_HORIZONTAL = 210;
    public const int A5H_SIZE_VERTICAL = 149;
    public const int A6_SIZE_HORIZONTAL = 105;
    public const int A6_SIZE_VERTICAL = 149;
    public const int A6H_SIZE_HORIZONTAL = 149;
    public const int A6H_SIZE_VERTICAL = 105;

    private DeviceManager manager;
    private DeviceInfo? selectedScanner;

    /// <summary>
    /// Image formats supported by scanning devices.
    /// </summary>
    public enum ImageFormat {
        JPEG = 0,
        BMP = 1,
        PNG = 2,
        GIF = 3,
        TIFF = 4,
    }

    /// <summary>
    /// Color modes supported by scanning devices.
    /// </summary>
    public enum ColorMode {
        Color = 1,
        GrayScale = 2,
        Text = 4,
    }

    /// <summary>
    /// Resolutions supported by scanning devices.
    /// </summary>
    public enum Resolution {
        Res_100 = 100,
        Res_150 = 150,
        Res_200 = 200,
        Res_300 = 300,
        Res_600 = 600,
    }


    public enum PageSize {
        A4,
        A5,
        A5H,
        A6,
        A6H,
    }

    /// <summary>
    /// Horizontal size for page formats.
    /// </summary>
    public enum HorizontalPageSize {
        A4 = 210,
        A5 = 149,
        A5_Horizontal = 210,
        A6 = 105,
        A6_Horizontal = 149,
    }

    /// <summary>
    /// Vertical size for page formats.
    /// </summary>
    public enum VerticalPageSize {
        A4 = 297,
        A5 = 210,
        A5_Horizontal = 149,
        A6 = 149,
        A6_Horizontal = 105,
    }



    public ScanningService() {
        manager = new DeviceManager();
    }


    public ScanningService(string deviceId) {
        manager = new DeviceManager();
        SelectScanner(deviceId);
    }


    /// <summary>
    /// Returns a list with available scanner devices.
    /// </summary>
    public List<Scanner> GetScannerList() {
        var list = new List<Scanner>();
        foreach (DeviceInfo info in manager.DeviceInfos) {
            if (info.Type == WiaDeviceType.ScannerDeviceType) {
                var name = info.Properties["Name"].get_Value().ToString();
                list.Add(new Scanner { DeviceId = info.DeviceID, Name = name });
            }
        }
        return list;
    }


    /// <summary>
    /// Starts scanning process in the device with given id using the given properties.<br/>
    /// </summary>
    /// <param name="deviceId">Device id of the scanning device.</param>
    /// <param name="properties">Scan properties for this process.</param>
    /// <returns>Byte array with the scanned image.</returns>
    public byte[] Scan(string deviceId, ScanProperties properties) {
        if (selectedScanner is null || selectedScanner.DeviceID != deviceId) {
            SelectScanner(deviceId);
            if (selectedScanner is null) return [];
        }
        Device device = selectedScanner.Connect();
        Item item = device.Items[1];
        SetPropertiesToItem(item, properties);
        ICommonDialog dialog = new CommonDialog();
        ImageFile imageFile = dialog.ShowTransfer(item, properties.ImageFormatId, false);
        if (imageFile is null) return [];
        var bytes = imageFile.FileData.get_BinaryData();
        return bytes;
    }


    public static void SetPageSize(PageSize pageSize, ScanProperties properties) {
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
    }


    /// <summary>
    /// Sets selectedScanner with the device with given id.<br/>
    /// If there is no device, sets null value.
    /// </summary>
    private void SelectScanner(string deviceId) {
        foreach (DeviceInfo info in manager.DeviceInfos) {
            if (info.DeviceID == deviceId) {
                selectedScanner = info;
            }
        }
    }


    /// <summary>
    /// Sets the propValue in item's property with propName name.
    /// </summary>
    private static void SetWIAProperty(Item item, object propName, object propValue) {
        Property prop = item.Properties.get_Item(ref propName);
        prop.set_Value(ref propValue);
    }


    private static void SetPropertiesToItem(Item item, ScanProperties properties) {
        SetWIAProperty(item, WIA_SCAN_COLOR_MODE, properties.ColorMode);
        SetWIAProperty(item, WIA_SCAN_BITS_PER_PIXEL, properties.BitsPerPixel);
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, properties.Resolution);
        SetWIAProperty(item, WIA_VERTICAL_SCAN_RESOLUTION_DPI, properties.Resolution);
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_START_PIXEL, MilimetersToPixels(properties.HorizontalStart, properties.HorizontalStart));
        SetWIAProperty(item, WIA_VERTICAL_SCAN_START_PIXEL, MilimetersToPixels(properties.VerticalStart, properties.VerticalStart));
        SetWIAProperty(item, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, MilimetersToPixels(properties.HorizontalSize, properties.Resolution));
        SetWIAProperty(item, WIA_VERTICAL_SCAN_SIZE_PIXELS, MilimetersToPixels(properties.VerticalSize, properties.Resolution));
        SetWIAProperty(item, WIA_SCAN_BRIGHTNESS_PERCENTS, properties.Brightness);
        SetWIAProperty(item, WIA_SCAN_CONTRAST_PERCENTS, properties.Contrast);
    }


    /// <summary>
    /// Gets the image format id for the ImageFormat selected.
    /// </summary>
    private static string GetImageFormatId(ImageFormat imageFormat) {
        return imageFormat switch {
            ImageFormat.JPEG => FORMAT_ID_JPEG,
            ImageFormat.BMP => FORMAT_ID_BMP,
            ImageFormat.PNG => FORMAT_ID_PNG,
            ImageFormat.GIF => FORMAT_ID_GIF,
            ImageFormat.TIFF => FORMAT_ID_TIFF,
            _ => FORMAT_ID_JPEG,
        };
    }


    /// <summary>
    /// Converts milimeters to pixels for given resolution.
    /// </summary>
    private static int MilimetersToPixels(int milimeters, int resolution) {
        var res = milimeters / 25.4m * resolution;
        return (int)Math.Round(res, 0);
    }

}
