'use strict';

var EventHubClient = require('azure-event-hubs').Client;

// Connection String to the IoTHub
// var connectionString = '{iothub connection string}';
var connectionString = 'HostName=oscheeriothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=k/BjpZ7WxfVwklj3f74o+ad1kvR/0qnJKPhyyBGgKdw=';


var printError = function (err) {
    console.log(err.message);
  };
  
  var printMessage = function (message) {
    console.log('Message received: ');
    console.log(JSON.stringify(message.body));
    console.log('');
  };

var client = EventHubClient.fromConnectionString(connectionString);
client.open()
    .then(client.getPartitionIds.bind(client))
    .then(function (partitionIds) {
        return partitionIds.map(function (partitionId) {
            return client.createReceiver('$Default', partitionId, { 'startAfterTime' : Date.now()}).then(function(receiver) {
                console.log('Created partition receiver: ' + partitionId)
                receiver.on('errorReceived', printError);
                receiver.on('message', printMessage);
            });
        });
    })
    .catch(printError);

    