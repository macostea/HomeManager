#include "dht_client.h"

#include <DHT_U.h>

#ifndef ARDUINODHTCLIENT_H
#define ARDUINODHTCLIENT_H

class ArduinoDHTClient : public DHTClient {
public:
    ArduinoDHTClient(const uint8_t pin, int type);
    virtual bool getEnvironment(Environment *environment);
    virtual void begin();
    virtual void end();
private:
    DHT_Unified *dht;
    uint8_t pin;
    int type;
};

#endif