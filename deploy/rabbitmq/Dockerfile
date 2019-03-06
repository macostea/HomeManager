FROM arm32v7/rabbitmq:management

RUN rabbitmq-plugins enable --offline rabbitmq_mqtt
COPY rabbitmq.conf /etc/rabbitmq/rabbitmq.conf

