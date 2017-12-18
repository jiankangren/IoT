
# Create your first IoT Edge module

1. Open VS Code

    Install from here ... 

1. Open Terminal in VS Code

    Select Folder where you want to 'code'

    Visual Studio Code --> View --> Integrated Terminal

1. Install Template for AzureIoTEdgeModule

    ```
    dotnet new -i Microsoft.Azure.IoT.Edge.Module
    ```

1. Create new Project

    ```
    dotnet new aziotedgemodule -n FilterModule
    or
    dotnet new aziotedgemodule -n SampleSensor
    ```

1. The Code (take it as it is first)

    ``` csharp
    using todo ...
    ```

1.  Build project
    go to  project-file --csproj
    context menu ---> BUild IOt Edge Module

1. Build Docker Image for Target platform

    context menu on docker file --> Build
    
    select publish folder as exe_dir 

    tag image-oscheeracr.azurecr.io/simulatedsensor:latest

1. Create ACR if not already exist


1. Login to ACR

    ``` 
    docker login -u <username> -p <password> <Login server>

    ``` 
1. Push to registry

    Push the image to your Docker repository. Use the View | Command Palette ... | Edge: Push IoT Edge module Docker image menu command and enter the image name in the pop-up text box at the top of the VS Code window.   Use the same image name you used in step 1.c.


1. Add registry credentials to Edge runtime on your Edge device

    For Windows, run the following command:
    ```
    iotedgectl login --address <docker-registry-address> --username <docker-username> --password <docker-password> 
    ```

    For Linux, run the following command:
    ```
    sudo iotedgectl login --address <docker-registry-address> --username <docker-username> --password <docker-password> 
    ```

1. Add 



## Tricks

### Let's start the IoT Edge runtime on start of Raspberry Pi



# Helpful Tools

## Device Explorer

