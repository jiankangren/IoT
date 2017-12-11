"""A simple sensor value simulation"""

import random
import threading
import timer
import time

UPDATE_INTERVAL = 60 # Seconds

def createsensorvalues():
    "This function creates three simulated sensor values"
    print "The result is  '%s' " % random.random()
    return

def timerfunction(f_stop):
    "runs every x seconds"
    createsensorvalues()
    if not f_stop.is_set():
        threading.Timer(UPDATE_INTERVAL, timerfunction, [f_stop]).start()

fstop = threading.Event()

timerfunction(fstop)

# createsensorvalues()

while 1:
    time.sleep(5)

print "Done :-)"
