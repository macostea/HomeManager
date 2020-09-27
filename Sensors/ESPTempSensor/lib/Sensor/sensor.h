#include <string>
#include "mqtt_client.h"
#include "dht_client.h"
#include "homey_client.h"

#ifndef SENSOR_H
#define SENSOR_H

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered,
    Sleepy,
    HomeyUnregistered,
    HomeyPublished
} SensorState;

class Sensor : MQTTClientDelegate {
public:
    std::string id;
    std::string type;
    Sensor(const std::string &id, const std::string &type, DHTClient *dhClient, MQTTClient *mqttClient, HomeyClient *homeyClient);

    void setup();
    void loop();
    SensorState getState();
    const std::string &getRoomId();

    void becomeSleepy();
    void homeyRegisterTimeout();

    virtual void mqttClientReceivedMessage(const std::string &topic, const std::string &message);
private:
    SensorState state;
    MQTTClient *mqttClient;
    DHTClient *dhtClient;
    HomeyClient *homeyClient;
    std::string roomId;

    void publishNewSensorMessage();
    void publishEnvironmentMessage();
    void publishHomeyMessage();
};

#endif