#include <string>
#include "mqtt_client.h"
#include "dht_client.h"
#include "hal.h"

#ifndef SENSOR_H
#define SENSOR_H

#define MAX_WAIT_TIME_FOR_RESPONSE_SEC 30

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered,
    Sleepy
} SensorState;

class Sensor : MQTTClientDelegate {
public:
    std::string id;
    std::string type;
    
    Sensor(const std::string &id, const std::string &type, HAL *hal);

    void setup();
    void stop();
    void loop();
    SensorState getState();
    const std::string &getRoomId();

    virtual void mqttClientReceivedMessage(const std::string &topic, const std::string &message);
private:
    SensorState state;
    HAL *hal;
    std::string roomId;

    unsigned long firstWaitTimeMillis;

    void publishNewSensorMessage();
    void publishEnvironmentMessage();
};

#endif