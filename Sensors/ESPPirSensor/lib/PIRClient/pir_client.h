#ifndef PIRCLIENT_H
#define PIRCLIENT_H

typedef struct Environment {
    bool motion;
} Environment;

class PIRClient {
public:   
    virtual void preventSleep(bool prevent) = 0;
};

#endif