#include "arduino_mqtt_client.h" 

static ArduinoMQTTClient *globalObj;

void subscribeCallback(char *data, uint16_t len) {
    auto delegate = globalObj->getDelegate();
    delegate->mqttClientReceivedMessage(globalObj->subscribedTopic, std::string(data));
}

ArduinoMQTTClient::ArduinoMQTTClient(Client *client, std::string host, int port, std::string username, std::string password) {
    this->mqttClient = new Adafruit_MQTT_Client(client, host.c_str(), port, username.c_str(), password.c_str());

    globalObj = this;
}

ArduinoMQTTClient::~ArduinoMQTTClient() {
    delete this->mqttClient;
}

bool ArduinoMQTTClient::connect() {
    int8_t ret;

    // Stop if already connected
    if (this->mqttClient->connected()) {
        return true;
    }

    uint8_t retries = 3;
    while ((ret = this->mqttClient->connect()) != 0) {
        this->mqttClient->disconnect();
        delay(5000);
        retries--;

        if (retries == 0) {
            return false;
        }
    }

    return true;
}

bool ArduinoMQTTClient::publish(std::string message, const std::string topic, int qos) {
    Adafruit_MQTT_Publish publisher(this->mqttClient, topic.c_str(), qos);

    return publisher.publish(message.c_str());
}

bool ArduinoMQTTClient::subscribe(std::string topic, int qos) {
    this->mqttSubscriber = new Adafruit_MQTT_Subscribe(this->mqttClient, topic.c_str(), qos);
    this->subscribedTopic = topic;
    this->mqttSubscriber->setCallback(&subscribeCallback);

    this->mqttClient->subscribe(this->mqttSubscriber);

    return true;
}

bool ArduinoMQTTClient::setDelegate(MQTTClientDelegate *delegate) {
    this->delegate = delegate;
    return true;
}

MQTTClientDelegate *ArduinoMQTTClient::getDelegate() {
    return this->delegate;
}

