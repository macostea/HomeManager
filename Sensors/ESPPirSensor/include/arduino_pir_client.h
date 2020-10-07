#include "pir_client.h"

#ifndef ARDUINOPIRCLIENT_H
#define ARDUINOPIRCLIENT_H

class ArduinoPIRClient: public PIRClient {
public:
    ArduinoPIRClient(int pin);
    ~ArduinoPIRClient();
    
    virtual void preventSleep(bool prevent);

private:
    int pin;
};

#endif