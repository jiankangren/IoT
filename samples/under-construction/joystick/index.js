var HID = require('node-hid');
var devices = HID.devices();


console.log(devices);

// { vendorId: 1035,
//     productId: 25907,
//     path: '\\\\?\\hid#vid_040b&pid_6533#7&394f08de&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}',
//     manufacturer: 'MOSIC     ',
//     product: 'SPEED-LINK Competition Pro ',
//     release: 256,
//     interface: -1,
//     usagePage: 1,
//     usage: 5 }

var device = new HID.HID("\\\\?\\hid#vid_040b&pid_6533#7&394f08de&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}");
// var device = new HID.HID("\\\\?\\hid#vid_040b&pid_6533#7&394f08de&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}");

var btn1 = 1; 
var btn2 = 2; 
var btn3 = 4; 
var btn4 = 8; 

device.on("data", function (data) {

    console.log("data: " + data);
    console.log("length: " + data.length);

    var str = "";
    var dataString = data.toString();
    for(var i = 0; i < dataString.length; i++) {
        str += dataString.charCodeAt(i) + "-";
    }
    console.log("chars: " + str);

    var buf = new Buffer.from(data.toString());

    console.log("data: " + buf.toString());
    // var temp = data.toString();

    var temp = buf.toString();
    var byte0 = temp.charCodeAt(0);
    var byte1 = temp.charCodeAt(1);
    var byte2 = temp.charCodeAt(2);

    // for(var i=0; i<temp.length; i++) {
        // var c = temp.charCodeAt(i);
        // console.log(c);

        if (byte2 | btn1 == btn1) {
            console.log("Button 1 pressed");
        }
        
        if (byte2 | btn2 == btn2) {
            console.log("Button 2 pressed");
        }

        // if (c & 0x02) {
        //     console.log("Button 2 pressed");
        // }

        // if (c & 0x04) {
        //     console.log("Button 3 pressed");
        // }

        // if (c & 0x08) {
        //     console.log("Button 4 pressed");
        // }
    // }

});