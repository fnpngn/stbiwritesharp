using StbiwSharp.Loader;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.InteropServices;
using static StbiwSharp.Loader.StbiwLoaderConfiguration;

namespace StbiwSharp
{
    //stb_image_write bindings for buffer writes
    public static partial class Stbiw
    {
        // There are five stbiw functions that use an arbitrary write function.

        /// <summary>
        /// Delegate used as a custom writer function
        /// </summary>
        /// <param name="context">Context <see cref="void*"/> that is passed to this callback from the original function call</param>
        /// <param name="data">Pointer to data that is returned for writing</param>
        /// <param name="size">Length of the given data</param>
        public delegate void stbi_write_func(IntPtr context, IntPtr data, int size);

        // Callback-based write functions
        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_png_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_png_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, IntPtr data, int stride_in_bytes);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_bmp_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_bmp_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_tga_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_tga_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_hdr_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_hdr_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, IntPtr data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_jpg_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_jpg_to_func(stbi_write_func func, IntPtr context, int x, int y, ImageChannels comp, IntPtr data, int quality);


        #region MarshaledBuffers
        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_png_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_png_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data, int stride_in_bytes);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_bmp_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_bmp_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_tga_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_tga_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_hdr_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_hdr_to_func(stbi_write_func func, IntPtr context, int w, int h, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] float[] data);

        [DllImport(STBIW_LIB, EntryPoint = "stbi_write_jpg_to_func", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int stbi_write_jpg_to_func(stbi_write_func func, IntPtr context, int x, int y, ImageChannels comp, [MarshalAs(UnmanagedType.LPArray)] byte[] data, int quality);
        #endregion


        public static unsafe int WritePngToStream(Stream stream, int width, int height, ImageChannels components, byte[] data)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            stbi_write_func callback = new stbi_write_func(WriteToStreamCallback);

            int result = 0;
            GCHandle handle = GCHandle.Alloc(stream);
            try
            {
                IntPtr context = GCHandle.ToIntPtr(handle);

                fixed (byte* ptr = data)
                {
                    result = stbi_write_png_to_func(callback, context, width, height, components, (IntPtr)ptr, stride);
                }
            }
            finally
            {
                handle.Free();
            }

            GC.KeepAlive(callback);
            return result;
        }

        public static unsafe int WriteBmpToStream(Stream stream, int width, int height, ImageChannels components, byte[] data)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            stbi_write_func callback = new stbi_write_func(WriteToStreamCallback);

            int result = 0;
            GCHandle handle = GCHandle.Alloc(stream);
            try
            {
                IntPtr context = GCHandle.ToIntPtr(handle);
                fixed (byte* ptr = data)
                {
                    result = stbi_write_bmp_to_func(callback, context, width, height, components, (IntPtr)ptr);
                }
            }
            finally
            {
                handle.Free();
            }

            GC.KeepAlive(callback);
            return result;
        }

        public static unsafe int WriteTgaToStream(Stream stream, int width, int height, ImageChannels components, byte[] data)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            stbi_write_func callback = new stbi_write_func(WriteToStreamCallback);

            int result = 0;
            GCHandle handle = GCHandle.Alloc(stream);
            try
            {
                IntPtr context = GCHandle.ToIntPtr(handle);
                fixed (byte* ptr = data)
                {
                    result = stbi_write_tga_to_func(callback, context, width, height, components, (IntPtr)ptr);
                }
            }
            finally
            {
                handle.Free();
            }

            GC.KeepAlive(callback);
            return result;
        }

        public static unsafe int WriteHdrToStream(Stream stream, int width, int height, ImageChannels components, float[] data)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            stbi_write_func callback = new stbi_write_func(WriteToStreamCallback);

            int result = 0;
            GCHandle handle = GCHandle.Alloc(stream);
            try
            {
                IntPtr context = GCHandle.ToIntPtr(handle);
                fixed (float* ptr = data)
                {
                    result = stbi_write_hdr_to_func(callback, context, width, height, components, (IntPtr)ptr);
                }
            }
            finally
            {
                handle.Free();
            }

            GC.KeepAlive(callback);
            return result;
        }

        public static unsafe int WriteJpgToStream(Stream stream, int width, int height, ImageChannels components, byte[] data, int quality)
        {
            int stride = sizeof(byte) * width * (int)components;

            if (data.Length != stride * height)
            {
                throw new ArgumentException("Invalid buffer size passed as " + nameof(data) + ": expected " + (width * height).ToString() + ", got " + data.Length.ToString());
            }

            stbi_write_func callback = new stbi_write_func(WriteToStreamCallback);

            int result = 0;
            GCHandle handle = GCHandle.Alloc(stream);
            try
            {
                IntPtr context = GCHandle.ToIntPtr(handle);
                fixed (byte* ptr = data)
                {
                    result = stbi_write_jpg_to_func(callback, context, width, height, components, (IntPtr)ptr, quality);
                }
            }
            finally
            {
                handle.Free();
            }

            GC.KeepAlive(callback);
            return result;
        }

        private static unsafe void WriteToStreamCallback(IntPtr context, IntPtr data, int size)
        {
            GCHandle handle = GCHandle.FromIntPtr(context);

            if (handle.Target is Stream stream)
            {
                ReadOnlySpan<byte> dataSpan = new ReadOnlySpan<byte>((void*)data, size);
                stream.Write(dataSpan);
            }
        }
    }
}
