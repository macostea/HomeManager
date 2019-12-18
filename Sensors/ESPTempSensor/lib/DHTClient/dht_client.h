#include <string>

typedef struct Environment {
    double temperature;
    double humidity;
} Environment;

class DHTClient {
public:
    virtual void getEnvironment(Environment *environment) = 0;
};
