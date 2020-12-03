#include <string>
#include "mqtt_client.h"
#include "pir_client.h"

#ifndef SENSOR_H
#define SENSOR_H

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered,
    PIRTimeout,
    Sleepy
} SensorState;

class Sensor : MQTTClientDelegate {
public:
    std::string id;
    std::string type;
    Sensor(const std::string &id, const std::string &type, MQTTClient *mqttClient, PIRClient *pirClient);

    void setup();
    void loop();
    SensorState getState();
    const std::string &getRoomId();

    void becomeSleepy();

    virtual void mqttClientReceivedMessage(const std::string &topic, const std::string &message);
private:
    SensorState state;
    MQTTClient *mqttClient;
    PIRClient *pirClient;
    std::string roomId;

    void publishNewSensorMessage();
    void publishEnvironmentMessage(bool motion);
};

#endif