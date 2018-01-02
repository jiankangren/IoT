FROM microsoft/dotnet:2.0.0-runtime-nanoserver-1709

ARG EXE_DIR=.

WORKDIR /app

COPY $EXE_DIR/ ./

CMD ["dotnet", "ultrasonic_module_python.dll"]