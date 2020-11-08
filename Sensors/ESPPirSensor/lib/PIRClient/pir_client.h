#ifndef PIRCLIENT_H
#define PIRCLIENT_H

class PIRClient {
public:
    virtual void begin() = 0;
    virtual void preventSleep(bool prevent) = 0;
};

#endif