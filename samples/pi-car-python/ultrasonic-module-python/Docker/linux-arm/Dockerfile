FROM resin/raspberrypi3-python

RUN apt-get install git -y 
RUN sudo pip install RPi.GPIO

RUN git clone --recursive https://github.com/sunfounder/SunFounder_PiCar.git
RUN python ./SunFounder_PiCar/setup.py install

COPY ultrasonictest.py ./
COPY Ultrasonic_Avoidance.pyc ./ 
COPY sensorhelper.py ./

CMD ["python", "ultrasonictest.py"] 