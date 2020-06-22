# Home Manager - SensorListener

This project is the MQTT gateway. It consumes messages from the MQTT broker (RabbitMQ) and uses the Sensor Service API to interact with the storage backend.

It also exposes it's own REST resources for notifications and control. Calls to these resources will be passed to the MQTT broker to finally reach the sensors.

# MQTT client
SensorListener uses the objects defined in [Listeners](Listeners/) to process incoming data from the MQTT broker.

These listeners are registered in Startup.cs

# REST API
SensorListener also exposes it's own REST resources. You can find these in [Controllers](Controllers/).

# Environment
|Name|Description|Default|
|---|---|---|
|SENSOR_SERVICE_URL|The URL for SensorService|localhost|
|RABBITMQ_HOST|The RabbitMQ URL|localhost|
|RABBITMQ_USERNAME|Username to connect to RabbitMQ|guest|
|RABBITMQ_PASSWORD|Password to connect to RabbitMQ|guest|
|RABBITMQ_EXCHANGE|RabbitMQ exchange name|SensorsExchange|
|RABBITMQ_QUEUE|RabbitMQ queue name|SensorsQueue|

