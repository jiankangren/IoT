FROM resin/raspberrypi3-python

RUN apt-get install git -y 
RUN sudo pip install RPi.GPIO

RUN git clone --recursive https://github.com/sunfounder/SunFounder_PiCar.git
RUN python ./SunFounder_PiCar/setup.py install

COPY ultrasonic-test.py ./
COPY Ultrasonic_Avoidance.pyc ./ 

CMD ["python", "ultrasonic-test.py"]