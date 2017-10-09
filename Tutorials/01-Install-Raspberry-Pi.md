# Install Raspberry Pi 2 (or 3) to simulate sensor data for Azure IoT Hub

In this tutorial you will learn to setup your Raspyberry Pi and simulate some sensor values, which you can send and analyze to the Azure IoT Hub.

Simulating sensor values abstracts the work with real sensors, you can add later. 

## Setting up the Raspberry PI

### Download Raspbian Image

Please download the latest version of Raspbian Desktop from the following website: 
[https://www.raspberrypi.org/downloads/raspbian/](https://www.raspberrypi.org/downloads/raspbian/)

### Download SD Card Tool Etcher

Do flash the image to your local SD Card pleas use the Etcher Tool. 

![Etcher - Tool to flash SD cards](images/etcher.png)

You can download Etcher from this link: [https://etcher.io/](https://etcher.io/)

The steps in th tool are very simple:
1. Select image
2. Select SD Card
3. Start

### Headless Installation

The headless installation reduce the need of connecting a keyboard or a monitor to the Raspberry Pi. This reduce the cost of hardware, and the cable chaos on your desk. 

To be able to connect to the Pi, you need SSH, which is disabled by default. To enable it before the first use of your SD Card. You just need to create an empty file in the root folder of the SD Card.

![Empty ssh file in rootfolder](images/ssh.png)

Now you can put the SD Card into your Pi. Please connect you Pi to your local area network with a cable to get a valid IP-Address. And don't forget to power on your device ;-)

### Find the IP-Address of the Raspberry Pi

To find the IP Address of your Raspberry Pi, you can use the [Advanced IP Scanner](http://www.advanced-ip-scanner.com/de/) to get a list of all "devices" in your network. 

![Advanced IP Scanner](images/advancedipscanner.png)

### Connect via ssh

I'm using [Putty](http://www.putty.org/) for ssh. With the IP-Address, Username pi and Password raspberry, you're ready to connect.

### Change Password is strongly recommended

Everyone knows the password of a new installed Raspberry Pi, so please changed it directly with the command:

``` 
$ passwd
``` 
### Update to latest version

Maybe the image is a few days old. To get the latest and greatest version of Raspbian use the following commands: 

```
$ sudo apt-get update
$ sudo apt-get $ dist-upgrade
```

### Configure access via remote desktop

To access your Raspberry via another computer over a remote desktop connection, you can use vnc (which is already installed in the Raspian Image). But before using it, you have to enable VNC via the Configuration of the "Interfaces".

![Enabling VNC](images/enablevnc.png)

After enabling VNC, a reboot is required.

Now you can access your Pi with the VNC Viewer from your local machine. You can install the VNC Viewer from here: [https://www.realvnc.com/en/connect/download/viewer/](https://www.realvnc.com/en/connect/download/viewer/)


### Create Hostname

For an easier access to your Pi, you should assign an unique hostname in the Pi Configuration Tool. 

![Using Raspberry Pi Configuration to set Hostname](images/sethostname.png)

A reboot is required after this step. 

### Create a Share for your source

To access the Pi via a file share, you have to install Samba. To do so, open a terminal window and type: 

```
sudo apt install -y samba
```

To configure samba: 

```  
sudo leafpad /etc/samba/smb.conf &
``` 

First Change [global] section 
``` 
[global]
  workgroup = WORKGROUP
  wins support = yes
``` 

The configure file share:

``` 
[pishare]
  comment=pishare
  path=/home/pi/
  browseable=yes
  writeable=yes
  only guest=no
  create mask=0777
  directory mask=0777
  public=no
``` 

Save the file and exit. 

Now add user "Pi" to the share and set file share password:

```
sudo smbpasswd -a pi
```

Restart samba server:
``` 
service smbd restart
``` 

Now you should be able to access the Pi over a file share in your Windows Explorer. Enter the following url in the Windows Explorer address bar.
\\\\yourhostnamehere\pishare

If it doesn't work, try 
\\\\youripaddress\pishare

DNS on Windows needs sometimes a while
