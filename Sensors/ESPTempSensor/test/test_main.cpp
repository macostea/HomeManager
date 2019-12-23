#include <unity.h>
#include "test_sensor.h"
#include "test_uuid_gen.h"

int main() {
    RUN_TEST(test_create_sensor);
    RUN_TEST(test_loop);

    RUN_TEST(test_generateUUID);

    UNITY_END();

    return 0;
}
