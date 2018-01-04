#!/usr/bin/env python
"""test app for sensor features"""

import time
import sensorhelper

# import Ultrasonic_Avoidance
# UA = Ultrasonic_Avoidance.Ultrasonic_Avoidance(20)
MY_DISTANCE_THRESHOLD = 10

def main():
    """main method"""
    # distance = UA.get_distance()
    distance = sensorhelper.get_distance()
    if distance != -1:
        message = 'Distance %s cm' % (distance)
        # print 'Distance', distance, 'cm'
        sensorhelper.log_message(message)
        time.sleep(0.2)
    else:
        print False

	#status = UA.less_than(threshold)
    status = sensorhelper.distance_less_than(MY_DISTANCE_THRESHOLD)

    if status == 1:
        # print "Less than %d" % MY_DISTANCE_THRESHOLD
        message = "Less than %d" % MY_DISTANCE_THRESHOLD
        sensorhelper.log_message(message)
    elif status == 0:
        # print "Over %d" % MY_DISTANCE_THRESHOLD
        message = "Over %d" % MY_DISTANCE_THRESHOLD
        sensorhelper.log_message(message)
    else:
        print "Read distance error."

if __name__ == '__main__':
    while True:
        main()
