# Scanning Service
**ScanningService** encapsulates the *WIA COM component* of Windows in a service.
I made this service for my own purpose. It's very simple and don't include all features of the *WIA COM component*. It only has the common features I need for scanning some images. If you need some features that I don't include, feel free to include it. Feel free to do whatever you want with this code. You can use and/or modify it in the way you want.

The usage is very simple and intuitive. Follow these small guidelines to implement the service in your application.
### Include ScanningService in your application
To include **ScanningService** in your application, you only has to add the service as singleton in the service collection.
```csharp
services.AddSingleton<ScanningService>();
```
### ScanProperties
*ScanProperties* is an object that has common use scanning parameters as resolution and page size.
**ScanningService** use millimeters for all sizes. The sizes are converted internally in pixeles using resolution for calculations.
```csharp
var properties = new ScanProperties {
    VerticalSize = ScanningService.A4_SIZE_VERTICAL,
    HorizontalSize = ScanningService.A4_SIZE_HORIZONTAL,
    Resolution = 200,
};
```
**ScanningService** has also constants with values for different page sizes, color modes and image formats.
### Scanner
To retrieve the list of available scanners you must call the *GetScannerList* method. You get a list of *Scanner* objects that has only two properties: *Name* and *DeviceId*. You use  *DeviceId* to scan an image from it using the method *Scan*.

### Scannning
Scanning with **ScanningService** is simple. You get the devices list and use one of them to scan.
```csharp
public class ScanViewModel {
	
	private readonly ScanningService scanningService;
	
	public ScanViewModel(ScanningService scanningService){
		this.scanningService = scanningService;
		Scanners = scanningService.GetScannerList();
		if (Scanners.Any()) SelectedScanner = Scanners[0];
	}

	public List<Scanner> Scanners { get; init; }

	public Scanner SelectedScanner { get; set; }

	public byte[] ScannedImage { get; set; }

	public void Scan(){
		var properties = new ScanProperties {
		    VerticalSize = ScanningService.A4_SIZE_VERTICAL,
		    HorizontalSize = ScanningService.A4_SIZE_HORIZONTAL,
		    Resolution = 200,
		};
		ScannedImage = scanningService.Scan(SelectedScanner.DeviceId, ScanProperties);
	}
}
```
In the example above, the service is injected via constructor and then *Scanners* has populated with all available devices and if there is any device, we select the first one as selected scanner. Then, in the *Scan* method, we create a *ScanProperties* object and set some of the values leaving the others in their default values. Then we set the result of *Scan* method in a byte array. The byte array has now all the content of the image. So simple.
### Extension methods
**ScanningService** has an extension method to set page size values in *ScanProperties* object in one step.
```csharp
public void Scan() {
	// Assuming we have a ScanProperties object called Properties.
	Properties.SetPageSize(ScanningService.PageSize.A4);
	ScannedImage = scanningService.Scan(SelectedScanner.DeviceId, Properties);
}

```