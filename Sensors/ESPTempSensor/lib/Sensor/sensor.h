#include <string>
#include "mqtt_client.h"
#include "dht_client.h"

typedef enum SensorState {
    New,
    WaitingResponse,
    Registered
} SensorState;

typedef void (*SensorCallback)();

class Sensor {
public:
    std::string id;
    std::string type;
    Sensor(std::string id, std::string type, DHTClient *dhClient, MQTTClient *mqttClient);

    void loop();
private:
    SensorState state;
    MQTTClient *mqttClient;
    DHTClient *dhtClient;
    std::string roomId;

    void processControlMessage(std::string message, SensorCallback callback);
    void controlMessageCallback(std::string message);

    void publishNewSensorMessage();
    void publishEnvironmentMessage();
};