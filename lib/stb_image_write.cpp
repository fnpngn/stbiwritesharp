#define STB_IMAGE_WRITE_IMPLEMENTATION

#if _WIN32
#define STBIWDEF extern "C" __declspec(dllexport) 
#endif

#include <stb_image_write.h>

// static accessors
#ifdef STB_IMAGE_WRITE_STATIC
STBIWDEF int get_stbi_write_tga_with_rle();	
STBIWDEF int get_stbi_write_png_compression_level();
STBIWDEF int get_stbi_write_force_png_filter();

STBIWDEF void set_stbi_write_tga_with_rle(int value);
STBIWDEF void set_stbi_write_png_compression_level(int value);
STBIWDEF void set_stbi_write_force_png_filter(int value);

#ifdef STB_IMAGE_WRITE_IMPLEMENTATION
STBIWDEF int get_stbi_write_tga_with_rle()
{
	return stbi_write_tga_with_rle;
}

STBIWDEF int get_stbi_write_png_compression_level()
{
	return stbi_write_png_compression_level;
}

STBIWDEF int get_stbi_write_force_png_filter()
{
	return stbi_write_force_png_filter;
}

STBIWDEF void set_stbi_write_tga_with_rle(int value)
{
	stbi_write_tga_with_rle = value;
}

STBIWDEF void set_stbi_write_png_compression_level(int value)
{
	stbi_write_png_compression_level = value;
}

STBIWDEF void set_stbi_write_force_png_filter(int value)
{
	stbi_write_force_png_filter = value;
}
#endif
#endif