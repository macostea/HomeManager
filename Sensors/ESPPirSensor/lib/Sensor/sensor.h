#include <string>
#include "mqtt_client.h"
#include "homey_client.h"
#include "pir_client.h"

#ifndef SENSOR_H
#define SENSOR_H

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered,
    PIRTimeout,
    Sleepy,
    HomeyUnregistered,
    HomeyPublished
} SensorState;

class Sensor : MQTTClientDelegate {
public:
    std::string id;
    std::string type;
    Sensor(const std::string &id, const std::string &type, MQTTClient *mqttClient, HomeyClient *homeyClient, PIRClient *pirClient);

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
    HomeyClient *homeyClient;
    PIRClient *pirClient;
    std::string roomId;

    void publishNewSensorMessage();
    void publishEnvironmentMessage(bool motion);
    void publishHomeyMessage(bool motion);
};

#endif