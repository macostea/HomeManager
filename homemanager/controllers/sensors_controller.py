import asyncio

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
    async def read_sensor(sensor):
        readings = await sensor.get_readings()

        return {
            "sensor": sensor,
            "readings": readings
        }

    async def read_sensors(self):
        tasks = []
        for sensor in self.sensors:
            tasks.append(asyncio.create_task(self.read_sensor(sensor)))

        done, pending = await asyncio.wait(tasks)
        print("done: ", done)
        print("pending: ", pending)

        sensor_data = [r.result() for r in done]

        return sensor_data

    def start_reading(self):
        self.interval = Interval(2, self.read_sensors)
        self.interval.start()

    def stop_reading(self):
        self.interval.stop()

        if self.executor is not None:
            self.executor.shutdown()
            self.executor = None
