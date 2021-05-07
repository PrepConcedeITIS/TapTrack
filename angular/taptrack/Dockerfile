FROM node:14.16-alpine as client

WORKDIR /app

ENV PATH /app/node_modulers/.bin:$PATH

COPY package.json /app/package.json
RUN npm install
RUN npm install -g @angular/cli@7.3.9

COPY . /app

CMD ng serve --host 0.0.0.0
