using StbiwSharp.Loader;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static StbiwSharp.Loader.StbiwLoaderConfiguration;

namespace StbiwSharp
{
    // added bindings for static configuration accessors
    public static partial class Stbiw
    {
        /// <summary>
        /// defaults to true <br/> set to <see cref="TgaRle.Disabled"/> to disable RLE
        /// </summary>
        public static TgaRle WriteTgaWithRle
        {
            get => get_stbi_write_tga_with_rle();
            set => set_stbi_write_tga_with_rle(value);
        }

        /// <summary>
        /// defaults to <see cref="PngCompressionLevel.Compression8"/> <br/> set to higher for more compression
        /// </summary>
        public static PngCompressionLevel PngCompressionLevel
        {
            get => get_stbi_write_png_compression_level();
            set => set_stbi_write_png_compression_level(value);
        }

        /// <summary>
        /// defaults to <see cref="PngFilter.Disabled"/> <br/> set to 0..5 to force a filter mode
        /// </summary>
        public static PngFilter ForcePngFilter
        {
            get => get_stbi_write_force_png_filter();
            set => set_stbi_write_force_png_filter(value);
        }


        [DllImport(STBIW_LIB, EntryPoint = "get_stbi_write_tga_with_rle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern TgaRle get_stbi_write_tga_with_rle();

        [DllImport(STBIW_LIB, EntryPoint = "get_stbi_write_png_compression_level", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PngCompressionLevel get_stbi_write_png_compression_level();

        [DllImport(STBIW_LIB, EntryPoint = "get_stbi_write_force_png_filter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PngFilter get_stbi_write_force_png_filter();

        [DllImport(STBIW_LIB, EntryPoint = "set_stbi_write_tga_with_rle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void set_stbi_write_tga_with_rle(TgaRle value);

        [DllImport(STBIW_LIB, EntryPoint = "set_stbi_write_png_compression_level", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void set_stbi_write_png_compression_level(PngCompressionLevel value);

        [DllImport(STBIW_LIB, EntryPoint = "set_stbi_write_force_png_filter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void set_stbi_write_force_png_filter(PngFilter value);
    }
}
