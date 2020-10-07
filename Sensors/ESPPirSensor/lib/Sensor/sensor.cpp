#include "sensor.h"

#include "ArduinoJson.h"

DynamicJsonDocument parseJson(std::string json) {
    const size_t capacity = JSON_OBJECT_SIZE(4) + 180;
    DynamicJsonDocument doc(capacity);

    deserializeJson(doc, json);
    return doc;
}

Sensor::Sensor(const std::string &id, const std::string &type, MQTTClient *mqttClient, HomeyClient *homeyClient, PIRClient *pirClient) {
    this->id = id;
    this->type = type;
    this->mqttClient = mqttClient;
    this->homeyClient = homeyClient;
    this->pirClient = pirClient;

    this->state = New;
}

SensorState Sensor::getState() {
    return this->state;
}

const std::string &Sensor::getRoomId() {
    return this->roomId;
}

void Sensor::setup() {
    this->mqttClient->setDelegate(this);
    this->mqttClient->subscribe(id, 1);
    this->homeyClient->begin(id);
}

void Sensor::loop() {
    switch (this->state)
    {
    case New:
        this->state = HomeyUnregistered;
        this->pirClient->preventSleep(true);
        break;

    case HomeyUnregistered:
        this->homeyClient->loop();
        break;

    case HomeyPublished:
        this->publishHomeyMessage(true);
        this->publishNewSensorMessage();
        this->state = WaitingResponse;
        break;

    case WaitingResponse:
        this->mqttClient->processPackets();
        break;

    case Registered:
        this->publishEnvironmentMessage(true);
        this->state = Sleepy;
        break;

    case Sleepy:
        this->pirClient->preventSleep(false);
        this->publishHomeyMessage(false);
        this->publishEnvironmentMessage(false);
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

void Sensor::publishEnvironmentMessage(bool motion) {
    Environment e;

    const size_t capacity = JSON_OBJECT_SIZE(2) + JSON_OBJECT_SIZE(3);
    DynamicJsonDocument doc(capacity);

    doc["sensorId"] = this->id.c_str();

    JsonObject environment = doc.createNestedObject("environment");
    environment["motion"] = motion;

    char msg[256];
    serializeJson(doc, msg);

    this->mqttClient->publish(msg, "environment", 1);
}

void Sensor::publishHomeyMessage(bool motion) {
    Environment e;

    this->homeyClient->updateMotion(motion);
}

void Sensor::mqttClientReceivedMessage(const std::string &topic, const std::string &message) {
    DynamicJsonDocument doc = parseJson(message);

    if (this->state == WaitingResponse) {
        this->roomId = std::string((const char *)doc["RoomId"]);
        this->state = Registered;
    }
}

void Sensor::becomeSleepy() {
    this->state = Sleepy;
}

void Sensor::homeyRegisterTimeout() {
    this->state = HomeyPublished;
}
