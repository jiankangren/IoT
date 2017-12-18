'use strict';

var clientFromConnectionString = require('azure-iot-device-mqtt').clientFromConnectionString;
var Message = require('azure-iot-device').Message;

// Connection String to the device
// var connectionString = 'HostName={youriothostname};DeviceId=myFirstNodeDevice;SharedAccessKey={yourdevicekey}';
var connectionString = "HostName=oscheeriothub.azure-devices.net;DeviceId=virtualnodejsdevice;SharedAccessKey=iNdMKBbxQXYV2Rt3tUAKt49H5eMbbpRglY8SCsky3y0=";

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
