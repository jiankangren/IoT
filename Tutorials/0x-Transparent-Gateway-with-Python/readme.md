# Use Python SDK on Raspberry Pi with IoT Edge and act as a transparent gateway

## Description

This scenario is using IoT Edge with Python applications to send data to the Azure IoT Hub, and receives data back from it. 

The challenge is, that the leaf device (a sensor on a raspberry pi SunFounder car for an example) should send and receive data through the IoT Edge Hub and not directly with the IoT Hub. The IoT Edge Hub will act as a transparent gateway. 

## Requirements

1. Install and start docker on device
1. Install Python 2.7 on device
1. Install VSCode and the Azure IoT Edge Extension on developer machine

## Steps to setup gateway device

### Create the certificates for test scenarios


1. Clone the [Microsoft Azure IoT SDKs and libraries for C] from GitHub:
    ``` 
    git clone -b modules-preview https://github.com/Azure/azure-iot-sdk-c.git
    ```

1. To install the certificate scripts, follow the instructions in Step 1 - Initial Setup of Managing CA Certificate Sample.

    I used the ps1 scripts in this folder to generate my certificates. It is based on the tutorial: [PowerShell scripts to manage CA-signed X.509 certificates](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-security-x509-create-certificates)

To generate the IoT hub owner CA, follow the instructions in Step 2 - Create the certificate chain. This file is used by the downstream devices to validate the connection.
To generate a certificate for your gateway device, use either the Bash or PowerShell instructions:

## More Documentation 

[How an IoT Edge device can be used as a gateway - preview](https://docs.microsoft.com/en-us/azure/iot-edge/iot-edge-as-gateway)

[Create an IoT Edge device that acts as a transparent gateway - preview](https://docs.microsoft.com/en-us/azure/iot-edge/how-to-create-transparent-gateway)

[Deploy Azure IoT Edge on a simulated device in Windows - preview](https://docs.microsoft.com/en-us/azure/iot-edge/tutorial-simulate-device-windows)

[Deploy Azure IoT Edge on a simulated device in Linux - preview](https://docs.microsoft.com/en-us/azure/iot-edge/tutorial-simulate-device-linux)

[Managing CA Certificates Sample](https://github.com/Azure/azure-iot-sdk-c/blob/modules-preview/tools/CACertificates/CACertificateOverview.md)

[Set up X.509 security in your Azure IoT hub](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-security-x509-get-started)

[PowerShell scripts to manage CA-signed X.509 certificates](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-security-x509-create-certificates)