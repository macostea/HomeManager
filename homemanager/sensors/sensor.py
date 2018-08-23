from abc import ABC, abstractmethod
from enum import Enum


class SensorType(Enum):
    WEATHER = 1
    TEMPERATURE = 2
    HUMIDITY = 3

    def __repr__(self):
        str_repr = {
            SensorType.WEATHER: "WEATHER",
            SensorType.TEMPERATURE: "TEMPERATURE",
            SensorType.HUMIDITY: "HUMIDITY"
        }

        return str_repr[self]


class Sensor(ABC):
    @property
    def types(self):
        return set()

    @property
    def name(self):
        return ""

    @abstractmethod
    async def get_readings(self):
        ...
