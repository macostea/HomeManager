#include <string>

#ifndef DHTCLIENT_H
#define DHTCLIENT_H

typedef struct Environment {
    double temperature;
    double humidity;
} Environment;

class DHTClient {
public:
    virtual bool getEnvironment(Environment *environment) = 0;
    virtual void begin() = 0;
};

#endif
