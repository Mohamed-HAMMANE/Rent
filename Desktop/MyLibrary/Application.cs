using System.Diagnostics;
using System.Reflection;
using System.Resources;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Application
    /// </summary>
    public static class _app
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="assembly">Assembly.GetExecutingAssembly()</param>
        /// <param name="resourceManager">resourceManager</param>
        public static void Init(Assembly assembly, ResourceManager resourceManager = null)
        {
            _session.Set("CurrentAssembly", FileVersionInfo.GetVersionInfo(assembly.Location));
            _session.Set("currentResourceManager", resourceManager);
            _session.Set("currentDirectoryPath", System.IO.Path.GetDirectoryName(assembly.Location));
        }

        /// <summary>
        /// FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location) of the app
        /// </summary>
        public static FileVersionInfo Data => _session.Get("CurrentAssembly") as FileVersionInfo;
        /// <summary>
        /// ProductVersion
        /// </summary>
        public static string Version => Data.ProductVersion;
        /// <summary>
        /// ProductName
        /// </summary>
        public static string Name => Data.ProductName;
        /// <summary>
        /// CompanyName
        /// </summary>
        public static string Company => Data.CompanyName;
        /// <summary>
        /// ResourceManager
        /// </summary>
        public static ResourceManager ResourceManager => _session.Get("currentResourceManager") as ResourceManager;
        /// <summary>
        /// The execution application's directory
        /// </summary>
        public static string CurrentDirectoryPath => _session.Get("currentDirectoryPath")?.ToString();
        /// <summary>
        /// FullName
        /// </summary>
        public static string FullName => $"{Company}#{Name}#{Version.Replace(".", "_")}";
    }
}
