import requests
import os
from threading import Timer, Lock

from homemanager.sensors.sensor import Sensor, SensorType
from homemanager.sensors.weather.icon_mapping import wunderground_icons


class WundergroundWeatherSensor(Sensor):
    __BASE_URL = "https://api.wunderground.com/api/"

    def __init__(self, location, icon_folder):
        self.__location = location
        self.__icon_folder = icon_folder
        self.__timer = None
        self.__current_conditions = None
        self.__lock = Lock()

    def timer_fired(self):
        self.__lock.acquire()

        self.__current_conditions = None
        self.__timer = None

        self.__lock.release()

    def icon_for_code(self, code):
        return os.path.join(self.__icon_folder, "wi-" + wunderground_icons[code] + ".svg")

    @property
    def name(self):
        return "WundergroundWeatherSensor"

    @property
    def types(self):
        return {SensorType.WEATHER}

    async def get_readings(self):
        self.__lock.acquire()

        if self.__current_conditions is None:
            url = "%s/%s/conditions/q/RO/%s.json" % (self.__BASE_URL, os.environ["WUNDERGROUND_API_KEY"], self.__location)
            result = requests.get(url)
            data = result.json()

            condition = data["current_observation"]

            self.__current_conditions = condition

            self.__timer = Timer(5 * 60 * 60, self.timer_fired)
            self.__timer.start()

        self.__lock.release()

        icon_code = self.__current_conditions["weather"].lower().replace(" ", "")

        return self.__current_conditions["temp_c"], self.__current_conditions["weather"], self.icon_for_code(icon_code)
