using Microsoft.Extensions.DependencyInjection;

namespace Ayri.ScanningService;

public static class ServiceCollectionExtensions {


    /// <summary>
    /// Adds an instance of ScanningService class as singleton in services collection.
    /// </summary>
    /// <returns>An reference to this instance after the service has been added.</returns>
    public static IServiceCollection AddScanningService(this IServiceCollection services) {
        services.AddSingleton<ScanningService>();
        return services;
    }


    /// <summary>
    /// Adds an instance of ScanningService ckass as singleton in services collection selecting the device with the given Id.
    /// </summary>
    /// <returns>An reference to this instance after the service has been added.</returns>
    public static IServiceCollection AddScanningService(this IServiceCollection services, string deviceId) {
        services.AddSingleton(new ScanningService(deviceId));
        return services;
    }

}
