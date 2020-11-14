using System.IO;
using System.Reflection;

namespace AcrCloudApiSdk.Helpers
{
    public static class Paths
    {
        public static string OutputDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string FingeprintsLocalDir => Path.Combine(OutputDirectory, @"Files\Fingerprints");
        public static string AcrcloudExtrExeTool => Path.Combine(OutputDirectory, @"Executables\acrcloud_extr_win.exe");
    }
}
