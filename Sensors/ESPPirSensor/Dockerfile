FROM python:3

RUN apt-get update && \
    apt-get install -y --no-install-recommends build-essential lcov && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

RUN pip install -U platformio
RUN platformio upgrade --dev

COPY . /app

WORKDIR /app

CMD ["bash", "/app/run-coverage.sh"]

