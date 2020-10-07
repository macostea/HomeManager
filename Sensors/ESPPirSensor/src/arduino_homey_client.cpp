#include "arduino_homey_client.h"


ArduinoHomeyClient::ArduinoHomeyClient() {
    this->client = new HomeyClass();
}

ArduinoHomeyClient::~ArduinoHomeyClient() {
    delete this->client;
}

bool ArduinoHomeyClient::begin(std::string id) {
    this->client->begin(String(id.c_str()));
    this->client->setClass("sensor");
    bool motionRes = this->client->addCapability("alarm_motion");

    return motionRes;
}

bool ArduinoHomeyClient::updateMotion(bool motion) {
    return this->client->setCapabilityValue("alarm_motion", motion);
}

bool ArduinoHomeyClient::loop() {
    return this->client->loop();
}
