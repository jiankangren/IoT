set subscriptionName="OScheer Azure Internal Consumption"
set resourceGroupName="oscheeriotrg"
set locationName="westeurope"
set iotHubName="oscheeriothub"


# Login
az login

# Get Account List
az account list

# Set Account
az account set --subscription "OScheer Azure Internal Consumption"

# Create resource group
az group create --name "oscheeriotrg" --location "westeurope"

# Create iot hub
az iot hub create --name "oscheeriothub" --resource-group "oscheeriotrg" --sku S1

az iot hub show-connection-string 


# list existing device
az iot device list --hub-name oscheeriothub

# create new device
az iot device create --hub-name oscheeriothub --device-id VirtualDevice002

# get connection string for device
az iot device show-connection-string --device-id VirtualDevice002 --hub-name oscheeriothub 

##### Clean up

# Delete IoT Hub
az iot hub delete --name "oscheeriothub" --resource-group "oscheeriotrg"

# Delete Resource Group

az group delete --name "oscheeriotrg"


