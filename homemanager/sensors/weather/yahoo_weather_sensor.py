import requests
import os
from threading import Timer, Lock

from homemanager.sensors.weather.icon_mapping import yahoo_icons


class YahooWeatherSensor:
    __BASE_URL = "https://query.yahooapis.com/v1/public/yql?"

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
        return os.path.join(self.__icon_folder, "wi-" + yahoo_icons[int(code)] + ".svg")

    def get_readings(self):
        self.__lock.acquire()

        if self.__current_conditions is not None:
            self.__lock.release()
            return int(self.__current_conditions["temp"]), str(self.__current_conditions["text"]), str(self.icon_for_code(self.__current_conditions["code"]))

        yql_query = "select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='%s') and u='c'" % self.__location
        result = requests.get(self.__BASE_URL, {"q": yql_query, "format": "json"})
        data = result.json()

        condition = data["query"]["results"]["channel"]["item"]["condition"]

        self.__current_conditions = condition

        self.__timer = Timer(5 * 60, self.timer_fired)
        self.__timer.start()

        self.__lock.release()

        return int(condition["temp"]), str(condition["text"]), str(self.icon_for_code(condition["code"]))

