#include "dht_client.h"
#include "mqtt_client.h"

#ifndef HAL_H
#define HAL_H

class HAL {
public:
    HAL(DHTClient *dhtClient, MQTTClient *mqttClient): dhtClient(dhtClient), mqttClient(mqttClient) {};
    HAL() {};
    virtual unsigned long getMillis() = 0;
    virtual DHTClient *getDHTClient() { return dhtClient; };
    virtual MQTTClient *getMQTTClient() { return mqttClient; };
    virtual std::string getUUID() = 0;

    virtual bool connect() = 0;

protected:
    DHTClient *dhtClient;
    MQTTClient *mqttClient;
};

#endif
