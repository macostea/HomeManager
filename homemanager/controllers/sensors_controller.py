import asyncio

from concurrent.futures import ThreadPoolExecutor, as_completed
from homemanager.utils.threading import Interval


class SensorsController:
    def __init__(self, *sensors):
        self.sensors = sensors
        self.callback = None
        self.interval = None
        self.executor = None

    def register_callback(self, callback):
        self.callback = callback

    @staticmethod
    def read_from_sensor(sensor):
        return sensor.get_readings()

    async def read_sensors_new(self):
        tasks = []
        for sensor in self.sensors:
            future = asyncio.Future()
            tasks.append(asyncio.ensure_future(sensor.get_readings(future)))

        done, pending = await asyncio.wait(tasks)
        print("done: ", done)
        print("pending: ", pending)

        sensor_data = [r.result() for r in done]

        return sensor_data

    def read_sensors(self):
        if self.executor is None:
            self.executor = ThreadPoolExecutor(max_workers=2)

        future_sensor_readings = {self.executor.submit(self.read_from_sensor, sensor): sensor for sensor in self.sensors}

        for future in as_completed(future_sensor_readings):
            sensor = future_sensor_readings[future]

            try:
                data = future.result()

                if self.callback is not None:
                    self.callback(sensor, data)

            except Exception:
                print("Failed to get sensor data for sensor " + sensor)

    def start_reading(self):
        self.interval = Interval(2, self.read_sensors)
        self.interval.start()

    def stop_reading(self):
        self.interval.stop()

        if self.executor is not None:
            self.executor.shutdown()
            self.executor = None

