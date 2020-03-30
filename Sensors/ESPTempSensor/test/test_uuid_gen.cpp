#include <unity.h>
#include "test_uuid_gen.h"
#include "uuid_gen.h"

void test_generateUUID() {
    std::string macAddr = "16:08:a8:04:4e:ed";
    std::string uuid = generateUUID(macAddr);

    TEST_ASSERT_EQUAL_STRING("686f6d65-6d61-6e61-6765-1608a8044eed", uuid.c_str());
    TEST_ASSERT_EQUAL(36, uuid.size());
}
