using System.Runtime.InteropServices;
using static StbiwSharp.Loader.StbiwLoaderConfiguration;

namespace StbiwSharp.Loader
{
    public class StbiwLoader
    {
        /// <summary>
        /// Method that should be called in static ctor of every dllimport class
        /// </summary>
        public static void LoadStbiw()
        {
            if (sIsStbiwLoaded)
            {
                return;
            }

            if (sLoadUserPath)
            {
                Load(sUserLibraryPath);
                sIsStbiwLoaded = true;
            }
            else
            {
                Load();
                sIsStbiwLoaded = true;
            }
        }

        private static void Load(string libraryFolderPath)
        {
            string libraryPath = Path.Combine(libraryFolderPath, GetPlatformLibraryPath());
            NativeLibrary.Load(libraryPath, typeof(StbiwLoader).Assembly, null);
        }

        private static void Load()
        {
            string libraryPath = GetPlatformLibraryPath();
            NativeLibrary.Load(libraryPath, typeof(StbiwLoader).Assembly, null);
        }

        private static string GetPlatformLibraryPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "stbiw.dll";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "libstbiw.so";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "libstbiw.dylib";
            throw new PlatformNotSupportedException();
        }
    }


}
