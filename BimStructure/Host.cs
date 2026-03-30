using Microsoft.Extensions.DependencyInjection;
using BimStructure.Configuration;
using BimStructure.Services;
using BimStructure.Views;
using BimStructure.ViewModels;

namespace BimStructure;

/// <summary>
///     Provides a host for the application's services and manages their lifetimes
/// </summary>
public static class Host
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    ///     Starts the host and configures the application's services
    /// </summary>
    public static void Start()
    {
        var services = new ServiceCollection();
        var pluginConfiguration = PluginConfiguration.Load();

        // Services
        services.AddSingleton(pluginConfiguration);
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IAccessDatabaseService, AccessDatabaseService>();

        //MVVM
        services.AddTransient<BimStructureViewModel>();
        services.AddTransient<BimStructureView>();
        services.AddTransient<DuAnMoiViewModel>();
        services.AddTransient<DuAnMoiView>();

        _serviceProvider = services.BuildServiceProvider();
    }

    /// <summary>
    ///     Get service of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The type of service object to get</typeparam>
    /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/></exception>
    public static T GetService<T>() where T : class
    {
        return _serviceProvider!.GetRequiredService<T>();
    }
}
