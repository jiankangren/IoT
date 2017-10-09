// HostName=syskron.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=xQTZ58Ozl5KD5svH4JqwUxT4CDBdRU9HH63HO33VyLw=
// myVirtualDevice001

var deviceId = 'myVirtualDevice001';
var connectionString = 'HostName=syskron.azure-devices.net;DeviceId=myVirtualDevice001;SharedAccessKey=LxZd7eRWiG1fCFtfjh8nLFQB3p7xVTdPauSNL5LT674=';

// Read values from local environment
var deviceId = process.env.AZURE_IOT_DEVICE_ID; 
var connectionString = process.env.AZURE_IOT_HUB_CONNECTIONSTRING;

console.log("Device Id        : " + deviceId)
console.log("Connection string: " + connectionString);

var clientFromConnectionString = require('azure-iot-device-http').clientFromConnectionString;

var client = clientFromConnectionString(connectionString);

var Message = require('azure-iot-device').Message;
var msg = new Message('some data from my device');

var updateIntervalInMilliseconds = 1000;

var connectCallback = function (err) {
  if (err) {
    console.error('Could not connect: ' + err);
  } else {
    console.log('Client connected');

    setInterval(sendData, updateIntervalInMilliseconds);

  
  }
};

client.open(connectCallback);

function sendData() {
  // console.log("sendData");
  
  var temperature = 20 + (Math.random() * 15);
  var humidity = 60 + (Math.random() * 20);            
  var data = JSON.stringify({ deviceId: deviceId, temperature: temperature, humidity: humidity });
    
  // var message = new Message('some data from my device');
  var message = new Message(data);

  client.sendEvent(message, function (err) {
    if (err) {
      console.log(err.toString());
    } else {
      console.log("send successful: " + data);
    }
  });

  // client.on('message', function (msg) {
  //   console.log(msg);
  //   client.complete(msg, function () {
  //     console.log('completed');
  //   });
  // });

  // console.log("sendData.done.");
}



// var message = new Message(data);
// message.properties.add('temperatureAlert', (temperature > 30) ? 'true' : 'false');
// console.log("Sending message: " + message.getData());
// client.sendEvent(message, printResultFor('send'));
