#include <string>

typedef void (*SubscribeCallbackType)(std::string message);

class MQTTClient {
public:
    virtual void connect() = 0;
    virtual void publish(std::string message, std::string topic, int qos) = 0;
    virtual void subscribe(std::string topic, int qos, SubscribeCallbackType callback) = 0;
};
