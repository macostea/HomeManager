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
    this->client->addCapability("measure_humidity");
    this->client->addCapability("measure_temperature");
}

bool ArduinoHomeyClient::updateTemperature(double temperature) {
    this->client->setCapabilityValue("measure_temperature", temperature);
}
bool ArduinoHomeyClient::updateHumidity(double humidity) {
    this->client->setCapabilityValue("measure_humidity", humidity);
}
bool ArduinoHomeyClient::loop() {
    this->client->loop();
}
