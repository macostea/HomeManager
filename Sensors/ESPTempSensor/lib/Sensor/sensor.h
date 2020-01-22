#include <string>
#include "mqtt_client.h"
#include "dht_client.h"

#ifndef SENSOR_H
#define SENSOR_H

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered
} SensorState;

class Sensor : MQTTClientDelegate {
public:
    std::string id;
    std::string type;
    Sensor(const std::string &id, const std::string &type, DHTClient *dhClient, MQTTClient *mqttClient);

    void setup();
    void loop();
    SensorState getState();
    const std::string &getRoomId();

    virtual void mqttClientReceivedMessage(const std::string &topic, const std::string &message);
private:
    SensorState state;
    MQTTClient *mqttClient;
    DHTClient *dhtClient;
    std::string roomId;

    void publishNewSensorMessage();
    void publishEnvironmentMessage();
};

#endif