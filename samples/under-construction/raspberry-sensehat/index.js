
'use strict';

var util = require('util')
var senseHat  = require('node-sense-hat');
var imu = senseHat.Imu;
var IMU = new imu.IMU();

var moment = require("moment");

var deviceId = 'RaspberryPi001';
var intervalPointer = undefined;

// Read values from local environment
// var connectionString = process.env.AZURE_IOT_HUB_DEVICE; 
var connectionString = "HostName=oscheeriothub.azure-devices.net;DeviceId=RaspberryPi001;SharedAccessKey=BB8/N7svqsde92f5motTaWeAnR6naHWwdCN+2IOszkQ="; 
logMessage("Device Connection String  : " + connectionString)
// Check if connection string is available
if (connectionString === undefined) {
  logError("Device Connection String not set correctly via Environment-Variable 'AZURE_IOT_HUB_DEVICE'.");
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
        pressure: 0
    }
};

function start() {
    client.open(function(err) {
        if (err) {
            logError('could not open IotHub client');
        }  else {
            logMessage('client opened');
      
            // TODO: Get current Device configuration ... if available

            // initDeviceTwin();

            // Get Device Twin
            client.getTwin(function(err, twin) {
                if (err) {
                    logError('could not get twin');
                } else {
                    deviceTwin = twin;
                    logMessage('retrieved device twin');
                    twin.on('properties.desired', function(desiredChange) {
                        logMessage("received change: " + JSON.stringify(desiredChange));
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
    //         logError('could not get twin');
    //     } else {
            deviceTwin.properties.reported.update(deviceValues.settings, function(err) {
                if (err) {
                    logError('could not update twin');
                } else {
                    logMessage('twin state reported');
                }
            });
        // }
    // });

    // client.getTwin(function(err, twin) {
    //     if (err) {
    //         logError('could not get twin');
    //     } else {
    //         twin.properties.reported.update(deviceValues.settings, function(err) {
    //             if (err) {
    //                 logError('could not update twin');
    //             } else {
    //                 logMessage('twin state reported');
    //             }
    //         });
    //     }
    // });

}

function updateSensorData() {
    // Simulated values 
    // var temperature = 20 + (Math.random() * 15);
    // var humidity = 60 + (Math.random() * 20);
    // var windspeed = Math.random() * 100;

    // deviceValues.sensors.temperature = temperature;
    // deviceValues.sensors.humidity = humidity;
    // deviceValues.sensors.windspeed = windspeed;

    // Getting real values from the Sense Hat 
    IMU.getValue(callb);


    // var data = JSON.stringify(deviceValues.sensors);
    // // var message = new Message(data);

    // var message = new Message(data);
  
    // client.sendEvent(message, function (err) {
    //   if (err) {
    //     logMessage("update deviceValues.sensors error: " + err.toString());
    //   } else {
    //     logMessage("update deviceValues.sensors successful: " + data);
    //   }
    // });
}

var print_vector3 = function(name, data) {
    var sx = data.x >= 0 ? ' ' : '';
    var sy = data.y >= 0 ? ' ' : '';
    var sz = data.z >= 0 ? ' ' : '';
    return util.format('%s: %s%s %s%s %s%s ', name, sx, data.x.toFixed(4), sy, data.y.toFixed(4), sz, data.z.toFixed(4));
}
  
var headingCorrection = function(heading, offset) {
    if (typeof offset ==='undefined')
        offset = 0;

    // Once you have your heading, you must then add your 'Declination Angle', which is the 'Error' of the magnetic field in your location.
    // Find yours here: http://www.magnetic-declination.com/
    var declinationAngle = 0.03106686;

    heading += declinationAngle + offset;

    // Correct for when signs are reversed.
    if (heading < 0)
        heading += 2 * Math.PI;

    // Check for wrap due to addition of declination.
    if (heading > 2 * Math.PI)
        heading -= 2 * Math.PI;

    return heading;
}

var headingToDegree = function(heading) {
// Convert radians to degrees for readability.
return heading * 180 / Math.PI;
}
  
  

var callb = function (e, data) {
    var toc = new Date();
  
    if (e) {
      logMessage(e);
      return;
    }
  
    // var str = print_vector3('Accel', data.accel)
    // str += print_vector3('Gyro', data.gyro)
    // str += print_vector3('Compass', data.compass)
    // str += print_vector3('Fusion', data.fusionPose)
    // str += util.format('TiltHeading: %s ', headingToDegree(headingCorrection(data.tiltHeading, Math.PI / 2)).toFixed(0));
  
    // var str2 = "";
    // if (data.temperature && data.pressure && data.humidity) {
    //   var str2 = util.format('Temp: %s Pressure: %s Humidity: %s', data.temperature.toFixed(4), data.pressure.toFixed(4), data.humidity.toFixed(4));
    // }
    // logMessage(str + str2);
  
    // setTimeout(function() { tic = new Date(); IMU.getValue(callb); } , 100 - (toc - tic));

    deviceValues.sensors.humidity = data.humidity.toFixed(4);
    deviceValues.sensors.temperature = data.temperature.toFixed(4);
    deviceValues.sensors.pressure = data.pressure.toFixed(4);

    var data = JSON.stringify(deviceValues.sensors);
    // var message = new Message(data);

    var message = new Message(data);
  
    client.sendEvent(message, function (err) {
      if (err) {
        logMessage("update deviceValues.sensors error: " + err.toString());
      } else {
        logMessage("update deviceValues.sensors successful: " + data);
      }
    });

  }
  
  
function startAndUpdateInterval() {
    
    if (intervalPointer != undefined) {
        logMessage("delete existing interval");
        clearInterval(intervalPointer);
    } 

    logMessage("start interval: " + deviceValues.settings.updateInterval + " ms");
    intervalPointer = setInterval(updateSensorData, deviceValues.settings.updateInterval);
}

var initConfigChange = function(twin) {
    logMessage("initConfigChange");

    // var reportedSettings = twin.properties.reported.settings;
    // logMessage("Reported settings: " + JSON.stringify(reportedSettings));
    
    var desiredSettings = twin.properties.desired;
    logMessage("Desired settings: " + JSON.stringify(desiredSettings));

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
        logMessage("Message: " + desiredSettings.Message);
    }

    // var patch = deviceValues.settings;
    twin.properties.reported.update(deviceValues.settings, function(err) {
    // twin.properties.reported.update(desiredSettings, function(err) {
        if (err) {
            logMessage('Could not report properties');
        } else {
            logMessage('Config changed: ' + JSON.stringify(deviceValues.settings));
        }
    });
}

start();

// 
// Log Features
// 

function logMessage(message) {
    var outputText = logCreateTimestampAndText(message);
    console.log(outputText);
}

function logError(message) {
    var outputText = logCreateTimestampAndText(message);
    console.error(outputText);
}

function logCreateTimestampAndText(message) {
    var formattedDateTime = moment().format("YYYY-MM-DD HH:mm:ss");
    var outputText = formattedDateTime + ": " + message;
    return outputText;
}