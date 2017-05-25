cmake_minimum_required(VERSION 3.5)

project(demo_nodes_cs)

find_package(ament_cmake REQUIRED)

add_custom_target(${PROJECT_NAME}_dll ALL
	COMMAND 
	msbuild ${PROJECT_NAME}.sln /property:OutputPath="${CMAKE_BINARY_DIR}" /property:Configuration=Release /property:Platform=x64
	WORKING_DIRECTORY
	${CMAKE_SOURCE_DIR}
)

install(FILES
	${CMAKE_BINARY_DIR}/talker_cs.exe
	${CMAKE_BINARY_DIR}/listener_cs.exe 
	DESTINATION bin)

ament_package()
