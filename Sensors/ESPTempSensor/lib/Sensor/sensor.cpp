#include "sensor.h"

#include "ArduinoJson.h"

DynamicJsonDocument parseJson(std::string json) {
    const size_t capacity = JSON_OBJECT_SIZE(4) + 180;
    DynamicJsonDocument doc(capacity);

    deserializeJson(doc, json);
    return doc;
}

Sensor::Sensor(const std::string &id, const std::string &type, DHTClient *dhtClient, MQTTClient *mqttClient) {
    this->id = id;
    this->type = type;
    this->mqttClient = mqttClient;
    this->dhtClient = dhtClient;

    this->state = New;
}

SensorState Sensor::getState() {
    return this->state;
}

const std::string &Sensor::getRoomId() {
    return this->roomId;
}

void Sensor::setup() {
    this->dhtClient->begin();
    this->mqttClient->setDelegate(this);
    this->mqttClient->subscribe(id, 1);
}

void Sensor::loop() {
    switch (this->state)
    {
    case New:
        this->publishNewSensorMessage();
        this->state = WaitingResponse;
        break;

    case WaitingResponse:
        this->mqttClient->processPackets();
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
    sensor["id"] = this->id.c_str();
    sensor["type"] = this->type.c_str();

    char msg[256];
    serializeJson(doc, msg);

    this->mqttClient->publish(msg, "sensor", 1);
}

void Sensor::publishEnvironmentMessage() {
    Environment e;
    this->dhtClient->getEnvironment(&e);

    const size_t capacity = JSON_OBJECT_SIZE(2) + JSON_OBJECT_SIZE(3);
    DynamicJsonDocument doc(capacity);

    doc["sensorId"] = this->id.c_str();

    JsonObject environment = doc.createNestedObject("environment");
    environment["temperature"] = e.temperature;
    environment["humidity"] = e.humidity;

    char msg[256];
    serializeJson(doc, msg);

    this->mqttClient->publish(msg, "environment", 1);
    this->state = Sleepy;
}

void Sensor::mqttClientReceivedMessage(const std::string &topic, const std::string &message) {
    DynamicJsonDocument doc = parseJson(message);

    if (this->state == WaitingResponse) {
        this->roomId = std::string((const char *)doc["RoomId"]);
        this->state = Registered;
    }
}
