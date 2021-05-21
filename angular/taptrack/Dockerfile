FROM node:15.3.0-alpine3.11 as build
WORKDIR /app
EXPOSE 4200

ENV PATH /app/node_modules/.bin:$PATH

COPY package.json /app/package.json
RUN npm install --save --legacy-peer-deps
RUN npm install -g @angular/cli
COPY . /app
CMD ng serve --host 0.0.0.0 -c production
