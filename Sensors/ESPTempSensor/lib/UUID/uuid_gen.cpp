#include "uuid_gen.h"

#include <algorithm>
#include <sstream>

std::string generateUUID(const std::string &mac) {
    std::string macCpy = mac;
    macCpy.erase(std::remove(macCpy.begin(), macCpy.end(), ':'), macCpy.end());

    std::stringstream buf;
    buf << "686f6d656d616e616765" << macCpy;
    return buf.str();
}
