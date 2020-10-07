#include <string>
#include "mqtt_client.h"

#ifndef HOMEYCLIENT_H
#define HOMEYCLIENT_H

class HomeyClient {
public:   
    virtual bool begin(std::string id) = 0;
    virtual bool updateTemperature(double temperature) = 0;
    virtual bool updateHumidity(double humidity) = 0;
    virtual bool loop() = 0;
};

#endif