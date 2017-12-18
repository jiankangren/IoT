'use strict';

var iothub = require('azure-iothub');

// Connection String to IoT Hub
// var connectionString = '{iothub connection string}';
var connectionString = 'HostName=oscheeriothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=k/BjpZ7WxfVwklj3f74o+ad1kvR/0qnJKPhyyBGgKdw=';

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
  