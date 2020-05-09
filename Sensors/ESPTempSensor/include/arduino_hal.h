#include "hal.h"

#include <ESP8266WiFi.h>

#ifndef ARDUINOHAL_H
#define ARDUINOHAL_H

class ArduinoHAL : public HAL {
public:
    ArduinoHAL();
    virtual unsigned long getMillis();
    virtual std::string getUUID();

    virtual bool connect();
private:
    WiFiClient *wifiClient;
};

#endif