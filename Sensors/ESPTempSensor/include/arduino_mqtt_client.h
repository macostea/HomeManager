#include <string>
#include <Adafruit_MQTT.h>
#include <Adafruit_MQTT_Client.h>
#include "mqtt_client.h"

#ifndef ARDUINOMQTTCLIENT_H
#define ARDUINOMQTTCLIENT_H

class ArduinoMQTTClient : public MQTTClient {
public:
    ArduinoMQTTClient(Client *client, std::string host, int port, std::string username, std::string password);
    ~ArduinoMQTTClient();
    
    virtual bool connect();
    virtual bool disconnect();
    virtual bool publish(std::string message, const std::string topic, int qos);
    virtual bool subscribe(std::string topic, int qos);
    virtual bool processPackets();

    virtual bool setDelegate(MQTTClientDelegate *delegate);
    MQTTClientDelegate *getDelegate();

    std::string subscribedTopic;
private:
    MQTTClientDelegate *delegate;
    Adafruit_MQTT_Client *mqttClient;
    Adafruit_MQTT_Subscribe *mqttSubscriber;

    std::string mqttHost;
    std::string mqttUsername;
    std::string mqttPassword;
    int mqttPort;
};

#endif