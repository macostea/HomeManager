FROM rabbitmq:4.2.3-management

RUN rabbitmq-plugins enable --offline rabbitmq_mqtt
COPY rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
RUN chown -R rabbitmq:rabbitmq /etc/rabbitmq

