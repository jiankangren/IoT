# Ultrasonic Module in Python for IoT Edge 

## Required Steps

When using a private Container Registry. Don't forget to login with 
```
sudo iotedgectl login --address <your container registry address> --username <username> --password <password>
```


Container Creation Options 
--> Privileged is required
```
{
  "HostConfig": {
    "Privileged" : true
  }
}
```


## Resources

[https://github.com/Azure-Samples/iot-edge-python-raspberrypi-connect-transparent-gateway/blob/master/README.md](https://github.com/Azure-Samples/iot-edge-python-raspberrypi-connect-transparent-gateway/blob/master/README.md)
