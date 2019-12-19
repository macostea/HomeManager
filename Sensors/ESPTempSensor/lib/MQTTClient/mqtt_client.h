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
    virtual void connect() = 0;
    virtual void publish(std::string message, const std::string topic, int qos) = 0;
    virtual void subscribe(std::string topic, int qos) = 0;

    virtual void addDelegate(const MQTTClientDelegate *delegate) = 0;
private:
    const MQTTClientDelegate *delegate;
};

#endif