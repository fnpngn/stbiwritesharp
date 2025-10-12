using StbiwSharp.Loader;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static StbiwSharp.Loader.StbiwLoaderConfiguration;

namespace StbiwSharp
{
    public static partial class Stbiw
    {
        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_png", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_png([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, IntPtr data, int stride_in_bytes);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_bmp", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_bmp([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_tga", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_tga([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_jpg", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_jpg([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, IntPtr data, int quality);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_hdr", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_hdr([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_flip_vertically_on_write", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void stbi_flip_vertically_on_write(StbiFlip flag);

        #region MarshaledBuffers
        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_png", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_png([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data, int stride_in_bytes);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_bmp", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_bmp([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_tga", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_tga([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_jpg", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_jpg([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data, int quality);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_hdr", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_hdr([MarshalAs(UnmanagedType.LPStr)] string filename, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] float[] data);
        #endregion

        /// <summary>
        /// Write a png file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename">Image file path</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="components">Number of components</param>
        /// <param name="data">Pixel data</param>
        /// <param name="stride">The distance in bytes from the first byte of a row of pixels <br/> 
        /// to the first byte of the next row of pixels. <br/>
        /// (should be width * channels * sizeof(<typeparamref name="T"/>))
        /// </param>
        /// <returns><see cref="true"/> if the operation succeeded</returns>
        /// <exception cref="ArgumentException">If invalid <paramref name="data"/> size is provided</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="data"/> is null</exception>
        public static unsafe bool WritePng<T>(string filename, int width, int height, ImageChannels components, T[] data, int stride) where T : struct
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            if (data.Length != width * height * (int)components)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            fixed (T* ptr = data)
            {
                int result = stbi_write_png(filename, width, height, components, (IntPtr)ptr, stride);

                return result != 0;
            }
        }

        /// <inheritdoc cref="WritePng{T}(string, int, int, ImageChannels, T[], int)"/>
        public static bool WritePng(string filename, int width, int height, ImageChannels components, byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            int result = stbi_write_png(filename, width, height, components, data, stride);

            return result != 0;
        }

        /// <inheritdoc cref="WritePng{T}(string, int, int, ImageChannels, T[], int)"/>
        public static unsafe bool WritePng(string filename, int width, int height, ImageChannels components, Span<byte> data)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            fixed (byte* ptr = data)
            {
                int result = stbi_write_png(filename, width, height, components, (IntPtr)ptr, stride);

                return result != 0;
            }
        }

        static Stbiw()
        {
            StbiwLoader.LoadStbiw();
        }
    }
}
