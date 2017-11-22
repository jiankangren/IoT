# Create a sensor simulator


## Install node.js

```
sudo apt-get install npm 
sudo npm install -g npm@2.x
```



## Create new nodejs project

npm init

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
iot-explorer login "<IoT Hub Connection string>"
iot-explorer create <DeviceName> --connection-string
```


## Add Connection String to Environment Variable AZURE_IOT_HUB_DEVICE



## Working with the device explorer





# Summary

Now you can replace the simulated values with your real sensors. 