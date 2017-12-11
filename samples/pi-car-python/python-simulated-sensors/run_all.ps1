# Build python base setup with libraries

# TODO: Update PIP

# python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=" -p < mqtt|http|amqp >

python simulatedsensors.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice002;SharedAccessKey=1BohJ/rjGRayqvGjtrvL9Fhwhsvoys62z7cxizeI5ko=" -p mqtt

python simulatedsensors.py -c 'HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=' -p mqtt

python simulatedsensors.py -c HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY= -p mqtt



python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice002;SharedAccessKey=1BohJ/rjGRayqvGjtrvL9Fhwhsvoys62z7cxizeI5ko=" -p mqtt



docker build -f "c:\dev\iot\samples\pi-car\python-simulated-sensors\dockerfile.pythonapp.ubuntu" --build-arg EXE_DIR="./samples/pi-car/python-simulated-sensors" -t "python-edge:latest" "c:\dev\iot"


https://rominirani.com/docker-on-windows-mounting-host-directories-d96f3f056a2c


# python001 - Connection String
# HostName=oscheeriothub.azure-devices.net;DeviceId=python001;SharedAccessKey=szqPF/FQjA/a7/V7wJ4Prm3NnDdZ1AV2CKl+sc9rFKg=

# Copy builded file 

docker cp 36b2b8ac4a44:/data/iothub_client.so ./pi-car/python-simulated-sensors
docker cp 36b2b8ac4a44:/data/iothub_client_args.py ./pi-car/python-simulated-sensors
docker cp 36b2b8ac4a44:/data/iothub_client_cert.py ./pi-car/python-simulated-sensors


#./usr/lib/x86_64-linux-gnu/libboost_python-py27.so.1.58.0

