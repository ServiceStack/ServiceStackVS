echo "Starting initial build..."

call jspm install

call typings install

msbuild "..\..\$safeprojectname$.sln" /p:configuration=debug

call jspm bundle deps deps.lib.js

echo "Finished initial build."