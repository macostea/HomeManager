#include "arduino_mqtt_client.h" 

static ArduinoMQTTClient *globalObj;

void subscribeCallback(char *data, uint16_t len) {
    Serial.println("Got message");
    Serial.println(data);
    auto delegate = globalObj->getDelegate();
    delegate->mqttClientReceivedMessage(globalObj->subscribedTopic, std::string(data));
}

ArduinoMQTTClient::ArduinoMQTTClient(Client *client, std::string host, int port, std::string username, std::string password) {
    mqttHost = host;
    mqttPort = port;
    mqttUsername = username;
    mqttPassword = password;

    this->mqttClient = new Adafruit_MQTT_Client(client, mqttHost.c_str(), mqttPort, mqttUsername.c_str(), mqttPassword.c_str());

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
    Serial.println("Sending message");
    Serial.println(message.c_str());
    Serial.println(topic.c_str());

    return publisher.publish(message.c_str());
}

bool ArduinoMQTTClient::subscribe(std::string topic, int qos) {
    Serial.println("Subscribing to topic");
    Serial.println(topic.c_str());
    this->subscribedTopic = topic;
    this->mqttSubscriber = new Adafruit_MQTT_Subscribe(this->mqttClient, this->subscribedTopic.c_str(), qos);
    this->mqttSubscriber->setCallback(&subscribeCallback);

    bool res = this->mqttClient->subscribe(this->mqttSubscriber);
    Serial.println("Subscription success");
    Serial.println(res);

    return res;
}

bool ArduinoMQTTClient::processPackets() {
    this->mqttClient->processPackets(1000);

    if (!this->mqttClient->ping()) {
        this->mqttClient->disconnect();
    }

    return true;
}

bool ArduinoMQTTClient::setDelegate(MQTTClientDelegate *delegate) {
    this->delegate = delegate;
    return true;
}

MQTTClientDelegate *ArduinoMQTTClient::getDelegate() {
    return this->delegate;
}
