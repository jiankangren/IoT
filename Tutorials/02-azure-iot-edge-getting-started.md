# Getting started with Azure IoT Edge

# Prerequisites

Software you could need:

## Setup IoT Hub

### Step 1: Create an Azure IoT Hub

Login to Azure portal and create a new Azure IoT Hub, if you don't have one. 

### Step x: Register an IoT Edge Device

Edge Explorer --> Create Device

# Setup on Raspberry PI

### Step x: Install IoT Edge runtime on device

```
sudo apt-get install python-pip
sudo apt-get install python2.7-dev libffi-dev libssl-dev
sudo pip install -U azure-iot-edge-runtime-ctl
```

### Step x: Configure IoT Edge runtime on device

```
sudo iotedgectl setup --connection-string "{device connection string}" --auto-cert-gen-force-no-passwords
```

### Step x: Start the IoT Edge runtime

```
sudo iotedgectl start
```

### Step x: Check Docker to see running IoT Edge Agent

```
sudo docker ps
```

# Setup on Windows 10

### Step x: Install Docker

### Step x: Install Python 2.7 on Windows

### Step x: Install Visual Studio Code

#### Required/Cool Extensions

Docker

Iot Edge 

### Step x: Install IoT Edge Control Script

```
pip install -U azure-iot-edge-runtime-ctl
```

### Step x: Register IoT Device

### Step x: Configure runtime

iotedgectl setup --connection-string {connectionstring} --auto-cert-gen-force-no-passwords

### Step x: Start Runtime

```
iotedgectl start

docker ps
```


# Install a module

1. In the Azure portal, navigate to your IoT hub.
1. Go to IoT Edge (preview) and select your IoT Edge device.
1. Select Set Modules.
1. Select Add IoT Edge Module.
1. In the Name field, enter tempSensor.
1. In the Image URI field, enter microsoft/azureiotedge-simulated-temperature-sensor:1.0-preview
1. Leave the other settings unchanged, and select Save.

1. Back in the Add modules step, select Next.
1. In the Specify routes step, select Next.
1. In the Review template step, select Submit.


### Check docker container

```
sudo docker ps

sudo docker ps --format 'table {{.Names}}\t{{.Image}}\t{{.Status}}'
```

To continuesly check a container use:

```
sudo docker logs -f tempSensor
```

