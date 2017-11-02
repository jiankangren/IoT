'use strict';

var deviceId = 'VirtualDevice001';
var intervalPointer = undefined;

// Read values from local environment
var connectionString = process.env.AZURE_IOT_HUB_DEVICE; 
console.log("Device Connection String  : " + connectionString)
// Check if connection string is available
if (connectionString === undefined) {
  console.error("Device Connection String not set correctly via Environment-Variable 'AZURE_IOT_HUB_DEVICE'.");
  return;
}

var Message = require('azure-iot-device').Message;
var Client = require('azure-iot-device').Client;
var Protocol = require('azure-iot-device-mqtt').Mqtt;
var client = Client.fromConnectionString(connectionString, Protocol);
var deviceTwin; 

// default values
var deviceValues = {
    settings: {
        updateInterval: 1000,
        enabled: true
    },
    sensors: {
        temperature: 0,
        humidity: 0,
        windspeed: 0
    }
};

function start() {
    client.open(function(err) {
        if (err) {
            console.error('could not open IotHub client');
        }  else {
            console.log('client opened');
      
            // TODO: Get current Device configuration ... if available

            // initDeviceTwin();

            // Get Device Twin
            client.getTwin(function(err, twin) {
                if (err) {
                    console.error('could not get twin');
                } else {
                    deviceTwin = twin;
                    console.log('retrieved device twin');
                    twin.on('properties.desired', function(desiredChange) {
                        console.log("received change: " + JSON.stringify(desiredChange));
                        initConfigChange(twin);
                    });
                }
            });

            // updateDeviceTwin();

            startAndUpdateInterval();
        }
    });

}

function updateDeviceTwin() {
    // client.getTwin(function(err, twin) {
    //     if (err) {
    //         console.error('could not get twin');
    //     } else {
            deviceTwin.properties.reported.update(deviceValues.settings, function(err) {
                if (err) {
                    console.error('could not update twin');
                } else {
                    console.log('twin state reported');
                }
            });
        // }
    // });

    // client.getTwin(function(err, twin) {
    //     if (err) {
    //         console.error('could not get twin');
    //     } else {
    //         twin.properties.reported.update(deviceValues.settings, function(err) {
    //             if (err) {
    //                 console.error('could not update twin');
    //             } else {
    //                 console.log('twin state reported');
    //             }
    //         });
    //     }
    // });

}

function updateSensorData() {
    // Simulated values 
    var temperature = 20 + (Math.random() * 15);
    var humidity = 60 + (Math.random() * 20);
    var windspeed = Math.random() * 100;

    deviceValues.sensors.temperature = temperature;
    deviceValues.sensors.humidity = humidity;
    deviceValues.sensors.windspeed = windspeed;

    var data = JSON.stringify(deviceValues.sensors);
    // var message = new Message(data);

    var message = new Message(data);
  
    client.sendEvent(message, function (err) {
      if (err) {
        console.log("update deviceValues.sensors error: " + err.toString());
      } else {
        console.log("update deviceValues.sensors successful: " + data);
      }
    });
}

  
function startAndUpdateInterval() {
    
    if (intervalPointer != undefined) {
        console.log("delete existing interval");
        clearInterval(intervalPointer);
    } 

    console.log("start interval: " + deviceValues.settings.updateInterval + " ms");
    intervalPointer = setInterval(updateSensorData, deviceValues.settings.updateInterval);
}

var initConfigChange = function(twin) {
    console.log("initConfigChange");

    // var reportedSettings = twin.properties.reported.settings;
    // console.log("Reported settings: " + JSON.stringify(reportedSettings));
    
    var desiredSettings = twin.properties.desired;
    console.log("Desired settings: " + JSON.stringify(desiredSettings));

    // reportedSettings.pendingConfig = twin.properties.desired.settings;
    // reportedSettings.status = "Pending";
    
    // Change updateInterval
    if (desiredSettings.updateInterval) {
        // Update Interval
        deviceValues.settings.updateInterval = desiredSettings.updateInterval;

        // restart interval;
        startAndUpdateInterval();
    }

    if (desiredSettings.Message) {
        Console.log("Message: " + desiredSettings.Message);
    }

    // var patch = deviceValues.settings;
    twin.properties.reported.update(deviceValues.settings, function(err) {
    // twin.properties.reported.update(desiredSettings, function(err) {
        if (err) {
            console.log('Could not report properties');
        } else {
            console.log('Config changed: ' + JSON.stringify(deviceValues.settings));
        }
    });
}

start();