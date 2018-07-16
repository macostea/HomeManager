try:
    import Adafruit_DHT
    dummy_sensor = False
except ModuleNotFoundError:
    dummy_sensor = True
    import random


import time


class DHTSensor:
    def __init__(self, **kwargs):

        if not dummy_sensor:
            self.sensor = Adafruit_DHT.DHT11
            self.pin = kwargs["pin"]

        self.result = None

    def get_readings(self, future):
        if dummy_sensor:
            time.sleep(0.9)
            future.set_result((random.random() * 50, random.random() * 50))
        else:
            future.set_result(Adafruit_DHT.read_retry(self.sensor, self.pin))

        return future
