cmake_minimum_required(VERSION 3.5.0)

project(stbiw)

add_library(stbiw SHARED stb_image_write.cpp)

#[[ set_target_properties(lib PROPERTIES PUBLIC_HEADER include/lib.h) ]]
set_target_properties(stbiw PROPERTIES PUBLIC_HEADER stb/stb_image_write.h)

target_include_directories(stbiw PRIVATE stb)

if(WIN32)
	set_target_properties(stbiw PROPERTIES PREFIX "")
endif()

install(TARGETS stbiw DESTINATION ${CMAKE_CURRENT_SOURCE_DIR}/../)