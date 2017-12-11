
From website

https://github.com/Azure/azure-iot-sdk-python/blob/master/doc/python-devbox-setup.md

1. install python
1. pip install azure-iothub-device-client
1. pip install azure-iothub-service-client
1. copy files from C:\dev\azure-iot-sdk-python\device\samples
1. start with
```
python iothub_client_sample.py -c HostName=<connectionstring> -p mqtt
```
python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=" -p mqtt

cat /usr/include/boost/version.hpp | grep "BOOST_LIB_VERSION"
-> 1.58

Hardlinks setzen