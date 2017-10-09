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


##### Clean up

# Delete IoT Hub
az iot hub delete --name "oscheeriothub" --resource-group "oscheeriotrg"

# Delete Resource Group

az group delete --name "oscheeriotrg"


