namespace StbiwSharp.Loader
{
    public static class StbiwLoaderConfiguration
    {
        public const string STBIW_LIB = "stbiw";

        internal static bool sLoadUserPath = false;
        internal static bool sIsStbiwLoaded = false;
        internal static string sUserLibraryPath = string.Empty;

        public static void UseCustomLibraryFolder(string libraryFolderPath)
        {
            sLoadUserPath = true;
            sUserLibraryPath = libraryFolderPath;
        }

        public static void UseDefaultImportPaths()
        {
            sLoadUserPath = false;
            sUserLibraryPath = "";
        }
    }
}
