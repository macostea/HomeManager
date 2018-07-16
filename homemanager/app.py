import os
from flask import Flask
from homemanager.controllers.sensors_controller import SensorsController


def create_sensors_controller():
    from homemanager.sensors.weather.wunderground_weather_sensor import WundergroundWeatherSensor
    from homemanager.controllers.sensors_controller import SensorsController
    from homemanager.sensors.dht.dht_sensor import DHTSensor

    sensors = [
        DHTSensor(pin=4),
        WundergroundWeatherSensor("cluj-napoca", os.path.join(os.path.dirname(os.path.realpath(__file__)), "resources", "weather-icons"))
    ]

    return SensorsController(*sensors)


def register_blueprints(app: Flask, sensors_controller: SensorsController):
    from homemanager.blueprints.api.sensors import create_blueprint as create_sensors_blueprint

    app.register_blueprint(create_sensors_blueprint(sensors_controller), url_prefix="/api/sensors")


def create_app(config_filename):
    app = Flask(__name__)
    # app.config.from_pyfile(config_filename)

    sensors_controller = create_sensors_controller()
    register_blueprints(app, sensors_controller)

    return app


if __name__ == '__main__':
    app = create_app()
    app.run()
