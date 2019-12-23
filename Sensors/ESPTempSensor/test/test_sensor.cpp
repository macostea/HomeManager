#include <unity.h>
#include "test_sensor.h"
#include "ArduinoJson.h"
#include "mqtt_client.h"
#include "dht_client.h"
#include "sensor.h"
#include "fakeit.hpp"

using namespace fakeit;

void test_create_sensor() {
    Mock<MQTTClient> mqttMock;
    Mock<DHTClient> dhtMock;

    When(Method(mqttMock, setDelegate)).Return();
    When(Method(mqttMock, subscribe)).Return();

    std::string id = "15D9083F-1349-4D67-921F-E186FC539E6C";

    Sensor s(id, "temp+hum", &dhtMock.get(), &mqttMock.get());

    TEST_ASSERT_EQUAL(s.getState(), New);
    VerifyNoOtherInvocations(mqttMock);
    VerifyNoOtherInvocations(dhtMock);
}

void test_loop() {
    Mock<MQTTClient> mqttMock;
    Mock<DHTClient> dhtMock;

    When(Method(mqttMock, setDelegate)).Return();
    When(Method(mqttMock, subscribe)).Return();

    std::string id = "15D9083F-1349-4D67-921F-E186FC539E6C";
    std::string type = "temp+hum";

    Sensor s(id, type, &dhtMock.get(), &mqttMock.get());

    When(Method(mqttMock, publish)).Return();

    // New -> WaitingResponse
    s.loop();

    TEST_ASSERT_EQUAL(s.getState(), WaitingResponse);

    size_t capacity = JSON_OBJECT_SIZE(1) + JSON_OBJECT_SIZE(2);
    DynamicJsonDocument doc(capacity);

    JsonObject sensor = doc.createNestedObject("sensor");
    sensor["id"] = id.c_str();
    sensor["type"] = type.c_str();

    std::string msg;
    serializeJson(doc, msg);

    Verify(Method(mqttMock, publish).Using(msg, id, 1));

    // WaitingResponse do nothing
    s.loop();
    VerifyNoOtherInvocations(mqttMock);

    // WaitingResponse -> Registered
    When(Method(dhtMock, getEnvironment)).Do([](Environment *e) -> bool {
        e->humidity = 100.0;
        e->temperature = 100.0;

        return 0;
    });

    When(Method(mqttMock, publish)).Return();

    s.mqttClientReceivedMessage(id, "{\"RoomId\": \"2993FBE3-46A0-419D-83CF-C514752B4F19\"}");
    TEST_ASSERT_EQUAL(s.getState(), Registered);
    TEST_ASSERT_EQUAL_STRING(s.getRoomId().c_str(), "2993FBE3-46A0-419D-83CF-C514752B4F19");

    s.loop();

    Verify(Method(dhtMock, getEnvironment));

    capacity = JSON_OBJECT_SIZE(2) + JSON_OBJECT_SIZE(3);
    DynamicJsonDocument doc2(capacity);

    doc2["sensorId"] = id.c_str();

    JsonObject environment = doc2.createNestedObject("environment");
    environment["temperature"] = 100.0;
    environment["humidity"] = 100.0;

    msg.clear();
    serializeJson(doc2, msg);

    Verify(Method(mqttMock, publish).Using(msg, id, 1));
}
