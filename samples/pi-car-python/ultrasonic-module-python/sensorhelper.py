#!/usr/bin/env python
"""Helper to access sensors"""

from datetime import datetime

import Ultrasonic_Avoidance
UA = Ultrasonic_Avoidance.Ultrasonic_Avoidance(20)

def get_distance():
    """get ultrasonic distance to object in centimeters"""
    distance = UA.get_distance()
    return distance

def distance_less_than(distancethreshold):
    """Is distance to next object less than threshold"""
    status = UA.less_than(distancethreshold)
    return status

def log_message(message):
    """helper to output text"""
    timestring = str(datetime.now())
    print timestring, message
