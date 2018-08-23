import asyncio
from flask import Blueprint, jsonify


from homemanager.controllers.sensors_controller import SensorsController


def create_blueprint(sensors_controller: SensorsController):
    sensors_page = Blueprint('sensors_page', __name__)

    @sensors_page.route('/')
    def get_sensor_data():
        loop = asyncio.new_event_loop()
        asyncio.set_event_loop(loop)
        sensor_data = loop.run_until_complete(sensors_controller.read_sensors())

        pretty_sensor_data = []
        for sensor_data_obj in sensor_data:
            sensor = sensor_data_obj["sensor"]

            pretty_sensor_data.append(
                {
                    "sensor": {
                        "name": sensor.name,
                        "types": [str(s) for s in sensor.types]
                    },
                    "readings": sensor_data_obj["readings"]
                }
            )

        return jsonify(pretty_sensor_data)

    return sensors_page
