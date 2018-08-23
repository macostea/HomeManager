from homemanager.sensors.sensor import Sensor, SensorType

try:
    import Adafruit_DHT
    dummy_sensor = False
except ModuleNotFoundError:
    dummy_sensor = True
    import random


import time


class DHTSensor(Sensor):
    def __init__(self, **kwargs):
        if not dummy_sensor:
            self.sensor = Adafruit_DHT.DHT11
            self.pin = kwargs["pin"]

        self.result = None

    @property
    def name(self):
        return "DHTSensor"

    @property
    def types(self):
        return {SensorType.HUMIDITY, SensorType.TEMPERATURE}

    async def get_readings(self):
        if dummy_sensor:
            time.sleep(0.9)
            return random.random() * 50, random.random() * 50
        else:
            return Adafruit_DHT.read_retry(self.sensor, self.pin)
