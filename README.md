# (WIP) Home Manager [![Build Status](https://dev.azure.com/mihaiandreicostea/HomeManager/_apis/build/status/macostea.HomeManager?branchName=master)](https://dev.azure.com/mihaiandreicostea/HomeManager/_build/latest?definitionId=2&branchName=master)
This project is a full feature IoT platform built to scale. You can run Home Manager anywhere from a single RPi on your local network to the cloud for huge scale.

# Disclaimer
Home Manager is a work-in-progress and it was mostly built as an educational tool for myself. It does not feature any security options for now 

# High-level architecture
* [TimescaleDB](https://www.timescale.com/) storage backend
* [REST service](SensorService/README.md) for data access
* REST <-> MQTT [gateway](SensorListener/README.md)
* [RabbitMQ](https://www.rabbitmq.com/) for sensor communication
* [Dashboard](Dashboard) for data visualization and control

# Installation
Home Manager can be installed using the helm chart in `deploy/home-manager`.

Adjust the values in `values.yaml` for your Kubernetes cluster.

Run the helm install command from `deploy/home-manager`:
```
$ helm upgrade --install --wait home-manager .
```

The helm chart contains all the necessary parts to run home-manager except the Dashboard. You can send sensor data over MQTT after the deploy is done and access the data using the SensorService.

# License
Home Manager is released under Apache License 2.0. See [LICENSE](LICENSE) for more information.

# Contact
Follow me on twitter [@mcostea](https://twitter.com/mcostea)