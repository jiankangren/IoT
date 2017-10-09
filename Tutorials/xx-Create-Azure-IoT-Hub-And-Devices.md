# Create an Azure IoT Hub 

This tutorial demonstrates an easy and fast way to generate an Azure IoT Hub using the Azure Commandline Tool --> AZ.

## Install AZ Commandline Tool

To use the following scripts, you need the Azure Commandline Tools. You can download and install them here: 

[https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest)


## Login to azure

Login to your Azure Account with the following command, and follow all instructions:

``` 
az login
```

If you have more than one active subscriptions, it is important to get the right subscription, where you want the Azure IoT Hub to be installed:

``` 
az account list
```

Pick the name of subscription for the Azure IoT Hub and insert it in the following command:

``` 
az account set --subscription <your subscription name>
```



``` 
az group create --name <resource group name --location <location>

```
Now it is time to create the Azure IoT Hub in the selected resource group

``` 
az iot hub create --name <iot hub name> --resource-group <resource group name> --sku S1
```

Finally, you need the Azure IoT Hub connection string, to create devices or explore messages.

``` 
az iot hub show-connection-string 
```
