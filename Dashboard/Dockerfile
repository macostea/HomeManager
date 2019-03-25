FROM node:11-alpine as build

RUN apk add git

WORKDIR /app
COPY package*.json /app/

RUN npm install

COPY . /app/
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/build/ /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

