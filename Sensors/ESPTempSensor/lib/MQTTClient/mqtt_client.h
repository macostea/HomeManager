#include <string>

#ifndef MQTTCLIENT_H
#define MQTTCLIENT_H

typedef void (*SubscribeCallbackType)(std::string message);

class MQTTClientDelegate {
public:
    virtual void mqttClientReceivedMessage(const std::string &topic, const std::string &message) = 0;
};

class MQTTClient {
public:
    virtual bool connect() = 0;
    virtual bool publish(std::string message, const std::string topic, int qos) = 0;
    virtual bool subscribe(std::string topic, int qos) = 0;
    virtual bool processPackets() = 0;

    virtual bool setDelegate(MQTTClientDelegate *delegate) = 0;
private:
    MQTTClientDelegate *delegate;
};

#endif