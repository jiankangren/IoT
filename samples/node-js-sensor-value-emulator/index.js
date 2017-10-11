'use strict';

// Read values from local environment
var deviceConnectionString = process.env.AZURE_IOT_HUB_DEVICE; 

console.log("Device Connection String  : " + deviceConnectionString)

if (deviceConnectionString === undefined) {
  console.error("Device Connection String not set correctly via Environment-Variable 'AZURE_IOT_HUB_DEVICE'.");
  return;
}

var clientFromConnectionString = require('azure-iot-device-http').clientFromConnectionString;
var client = clientFromConnectionString(deviceConnectionString);
var Message = require('azure-iot-device').Message;

// Update interval in milliseconds of sensor values 
var updateIntervalInMilliseconds = 5000;

var initConfigChange = function(twin) {
  var currentTelemetryConfig = twin.properties.reported.telemetryConfig;
  currentTelemetryConfig.pendingConfig = twin.properties.desired.telemetryConfig;
  currentTelemetryConfig.status = "Pending";

  var patch = {
  telemetryConfig: currentTelemetryConfig
  };
  twin.properties.reported.update(patch, function(err) {
      if (err) {
          console.log('Could not report properties');
      } else {
          console.log('Reported pending config change: ' + JSON.stringify(patch));
          setTimeout(function() {completeConfigChange(twin);}, 60000);
      }
  });
}

var completeConfigChange =  function(twin) {
  var currentTelemetryConfig = twin.properties.reported.telemetryConfig;
  currentTelemetryConfig.configId = currentTelemetryConfig.pendingConfig.configId;
  currentTelemetryConfig.sendFrequency = currentTelemetryConfig.pendingConfig.sendFrequency;
  currentTelemetryConfig.status = "Success";
  delete currentTelemetryConfig.pendingConfig;

  var patch = {
      telemetryConfig: currentTelemetryConfig
  };
  patch.telemetryConfig.pendingConfig = null;

  twin.properties.reported.update(patch, function(err) {
      if (err) {
          console.error('Error reporting properties: ' + err);
      } else {
          console.log('Reported completed config change: ' + JSON.stringify(patch));
      }
  });
};


client.open(
  function (err) {
    if (err) {
      // Error
      console.error('Could not connect to IoT Hub: ' + err);
    } else {
      // Success
      console.log('Client connected');
      // register Device Twin
      client.getTwin(function () {
        if (err) {
          console.error("could not get twin");
        } else {
          console.log("retrieved device twin");
          twin.properties.reported.telemetryConfig = {
              configId: "0",
              sendFrequency: "24h"
          }
          twin.on('properties.desired', function(desiredChange) {
            console.log("received change: "+JSON.stringify(desiredChange));
            var currentTelemetryConfig = twin.properties.reported.telemetryConfig;
              if (desiredChange.telemetryConfig &&desiredChange.telemetryConfig.configId !== currentTelemetryConfig.configId) {
                  initConfigChange(twin);
              }
          });

        }
      });
      // Start processing 
      startAndUpdateInterval(updateIntervalInMilliseconds);
    }
  });

function startAndUpdateInterval(updateIntervalInMilliseconds) {
  setInterval(sendData, updateIntervalInMilliseconds);
}

function sendData() {
  // Simulated values 
  var temperature = 20 + (Math.random() * 15);
  var humidity = 60 + (Math.random() * 20);            
  var data = JSON.stringify({ temperature: temperature, humidity: humidity });
    
  var message = new Message(data);

  client.sendEvent(message, function (err) {
    if (err) {
      console.log(err.toString());
    } else {
      console.log("send successful: " + data);
    }
  });
}
