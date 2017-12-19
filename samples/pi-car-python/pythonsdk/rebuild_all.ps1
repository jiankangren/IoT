# Step 1: Create build machine
docker build --rm -f dockerfile1 -t pythonsdk1:latest .

# Step 2: Clone repository
docker build --rm -f dockerfile2 -t pythonsdk2:latest .

# Step 3: setup prerequisites for build script
docker build --rm -f dockerfile3 -t pythonsdk3:latest .

# Step 4: build
docker build --rm -f dockerfile4 -t pythonsdk4:latest .

# Run image once
docker run -it pythonsdk4:latest

# create output directory
# md c:\outputsdk

# copy files out of container --> Use docker ps to get id
# docker cp a670bf074050:/outputsdk/. outputsdk

# run sample outside of docker works
# pyc files are generated

# step 5a: Run the app
docker build --rm -f dockerfile5 -t pythonsdk5:latest .
docker run -it pythonsdk5:latest

# Step 5b: in docker 
# python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=" -p mqtt

# python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=" -p http

# python iothub_client_sample.py -c "HostName=oscheeriothub.azure-devices.net;DeviceId=pythondevice001;SharedAccessKey=N+7DT1fDKGurce2xHBA587lY/1UMfYS53seta6eM7VY=" -p amqp
