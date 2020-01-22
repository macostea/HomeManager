#include <unity.h>
#include "test_uuid_gen.h"
#include "uuid_gen.h"

void test_generateUUID() {
    std::string macAddr = "16:08:a8:04:4e:ed";
    std::string uuid = generateUUID(macAddr);

    TEST_ASSERT_EQUAL_STRING("686f6d656d616e6167651608a8044eed", uuid.c_str());
    TEST_ASSERT_EQUAL(32, uuid.size());
}
