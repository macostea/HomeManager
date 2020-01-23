FROM python:3

RUN apt-get update && \
    apt-get install -y build-essential lcov

RUN pip install -U platformio
RUN platformio upgrade --dev

ADD . /app

WORKDIR /app

CMD ["bash", "/app/run-coverage.sh"]

