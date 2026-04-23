using System.Configuration;
using System.IO;
using System.Reflection;

namespace BimStructure.Configurations;

public sealed class PluginConfiguration
{
    public string AccessProvider { get; set; } = "Microsoft.ACE.OLEDB.12.0";

    public string AccessConnectionStringTemplate { get; set; } =
        "Provider={0};Data Source={1};Persist Security Info=False;";

    public static PluginConfiguration Load()
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? AppDomain.CurrentDomain.BaseDirectory;
        var configPath = Path.Combine(assemblyDirectory, "BimStructure.config");

        if (!File.Exists(configPath))
        {
            return new PluginConfiguration();
        }

        var fileMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = configPath
        };

        var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        var appSettings = configuration.AppSettings.Settings;

        return new PluginConfiguration
        {
            AccessProvider = appSettings["AccessProvider"]?.Value ?? "Microsoft.ACE.OLEDB.12.0",
            AccessConnectionStringTemplate = appSettings["AccessConnectionStringTemplate"]?.Value
                ?? "Provider={0};Data Source={1};Persist Security Info=False;"
        };
    }
}
