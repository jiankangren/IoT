# Create a node.js sensor simulator 

In this tutorial, you will learn to create a small node.js app to simulate sensor values from a virtual device.

A long version of this tutorial can be found here: [https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-node-node-getstarted](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-node-node-getstarted)

## Step x: Create Azure IoT Hub

1. Login to your Azure Subscription  
1. Create/Go To you Azure IoT Hub


## Step x: create a device id on portal
1. Go To IoT Devices in your IoT Hub blade
1. Create new Device called 'virtualnodejsdevice'

Save your Connection String for later
 

## Step x: node.js to create device id Create node.js app

Open a terminal in your working folder

```
node init 
```

Name the App 'nodesimulator'

Add required package
```
npm install azure-iothub --save
```

1. Add a file called 'CreateDeviceIdentity.js'

TODO: CODE Here

## Step x: ReadDeviceToCloudMessage.js

1. Add package
```
npm install azure-event-hubs --save
```
1. Add a file called 'ReadDeviceToCloudMessage.js'

Complete Code

```
'use strict';

var iothub = require('azure-iothub');

// Connection String to IoT Hub
var connectionString = '{iothub connection string}';

var registry = iothub.Registry.fromConnectionString(connectionString);

var device = {
    deviceId: 'myFirstNodeDevice'
  }
  registry.create(device, function(err, deviceInfo, res) {
    if (err) {
      registry.get(device.deviceId, printDeviceInfo);
    }
    if (deviceInfo) {
      printDeviceInfo(err, deviceInfo, res)
    }
  });
  
  function printDeviceInfo(err, deviceInfo, res) {
    if (deviceInfo) {
      console.log('Device ID: ' + deviceInfo.deviceId);
      console.log('Device key: ' + deviceInfo.authentication.symmetricKey.primaryKey);
    }
  }
```



## Step x: SimulatedDevice.js 

1. Add a file called 'SimulatedDevice.js'

```
npm install azure-iot-device azure-iot-device-mqtt --save
``` 

Complete Code: 
```
'use strict';

var clientFromConnectionString = require('azure-iot-device-mqtt').clientFromConnectionString;
var Message = require('azure-iot-device').Message;

// Connection String to the device
var connectionString = 'HostName={youriothostname};DeviceId=myFirstNodeDevice;SharedAccessKey={yourdevicekey}';

var client = clientFromConnectionString(connectionString);

var intervalInMilliseconds = 5000; // 5 Seconds

function printResultFor(op) {
    return function printResult(err, res) {
      if (err) console.log(op + ' error: ' + err.toString());
      if (res) console.log(op + ' status: ' + res.constructor.name);
    };
  }

  var connectCallback = function (err) {
    if (err) {
      console.log('Could not connect: ' + err);
    } else {
      console.log('Client connected');
  
      // Create a message and send it to the IoT Hub every second
      setInterval(function(){
          var temperature = 20 + (Math.random() * 15);
          var humidity = 60 + (Math.random() * 20);            
          var data = JSON.stringify({ deviceId: 'myFirstNodeDevice', temperature: temperature, humidity: humidity });
          var message = new Message(data);
          message.properties.add('temperatureAlert', (temperature > 30) ? 'true' : 'false');
          console.log("Sending message: " + message.getData());
          client.sendEvent(message, printResultFor('send'));
      }, intervalInMilliseconds);
    }
  };

  client.open(connectCallback);

```

## Step x: 

Start ReadDeviceToCloudMessage.js in a new terminal window
``` 
node ReadDeviceToCloudMessage.js
```

Start SimulatedDevice.sj in a new terminal window
``` 
node SimulatedDevice.js
```
