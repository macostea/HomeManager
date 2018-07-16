import asyncio
from flask import Blueprint, jsonify


from homemanager.controllers.sensors_controller import SensorsController


def create_blueprint(sensors_controller: SensorsController):
    sensors_page = Blueprint('sensors_page', __name__)

    @sensors_page.route('/')
    def get_sensor_data():
        loop = asyncio.new_event_loop()
        asyncio.set_event_loop(loop)
        sensor_data = loop.run_until_complete(sensors_controller.read_sensors_new())
        return jsonify(sensor_data)

    return sensors_page
