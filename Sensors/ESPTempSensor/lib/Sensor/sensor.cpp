#include "sensor.h"

#include <ArduinoJson.h>

DynamicJsonDocument parseJson(std::string json) {
    const size_t capacity = JSON_OBJECT_SIZE(4) + 180;
    DynamicJsonDocument doc(capacity);

    deserializeJson(doc, json);
    return doc;
}

Sensor::Sensor(std::string id, std::string type, DHTClient *dhtClient, MQTTClient *mqttClient) {
    this->id = id;
    this->type = type;
    this->mqttClient = mqttClient;
    this->dhtClient = dhtClient;

    mqttClient->subscribe(id, 1, &this->controlMessageCallback);
}

void Sensor::loop() {
    switch (this->state)
    {
    case New:
        this->publishNewSensorMessage();
        this->state = WaitingResponse;
        break;

    case WaitingResponse:
        break;

    case Registered:
        this->publishEnvironmentMessage();
        break;

    default:
        break;
    }
}

void Sensor::publishNewSensorMessage() {
    const size_t capacity = JSON_OBJECT_SIZE(1) + JSON_OBJECT_SIZE(2);
    DynamicJsonDocument doc(capacity);

    JsonObject sensor = doc.createNestedObject("sensor");
    sensor["id"] = this->id;
    sensor["type"] = this->type;

    std::string msg;
    serializeJson(doc, msg);

    this->mqttClient->publish(msg, this->id, 1);
}

void Sensor::publishEnvironmentMessage() {
    Environment e;
    this->dhtClient->getEnvironment(&e);

    const size_t capacity = JSON_OBJECT_SIZE(2) + JSON_OBJECT_SIZE(3);
    DynamicJsonDocument doc(capacity);

    doc["sensorId"] = this->id;

    JsonObject environment = doc.createNestedObject("environment");
    environment["temperature"] = e.temperature;
    environment["humidity"] = e.humidity;

    std::string msg;
    serializeJson(doc, msg);

    this->mqttClient->publish(msg, this->id, 1);
}

void Sensor::controlMessageCallback(std::string message) {
    DynamicJsonDocument doc = parseJson(message);

    if (this->state == WaitingResponse) {
        this->roomId = std::string((char *)doc["RoomId"]);
        this->state = Registered;
    }
}
