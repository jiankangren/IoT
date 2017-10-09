# Create a sensor simulator


## Install node.js

```
sudo apt-get install npm 
sudo npm install -g npm@2.x
```

## Packages for IoT Hub communication

```
sudo npm install -g azure-iot-device@latest
sudo npm install -g azure-iot-device-http@latest
```

## Installing IoT-Explorer

```
sudo npm install -g iot-explorer@latest
```

## Create a new device

```
Iot-explorer login "<IoT Hub Connection string>"
Iot-explorer create <DeviceName> --connection-string
```

## Working with the device explorer

# Summary

Now you can replace the simulated values with your real sensors. 