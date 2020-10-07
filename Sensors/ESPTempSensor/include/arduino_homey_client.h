#include <string>
#include "homey_client.h"
#include <Homey.h>

#ifndef ARDUINOHOMEYCLIENT_H
#define ARDUINOHOMEYCLIENT_H

class ArduinoHomeyClient: public HomeyClient {
public:
    ArduinoHomeyClient();
    ~ArduinoHomeyClient();
    
    virtual bool begin(std::string id);
    virtual bool updateTemperature(double temperature);
    virtual bool updateHumidity(double humidity);
    virtual bool loop();
private:
    HomeyClass *client;
};

#endif