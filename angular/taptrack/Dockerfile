FROM node:15.3.0-alpine3.11 as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY package.json .
RUN npm install
COPY . .
RUN ng build --prod || true

FROM nginx as runtime
COPY --from=build /app/dist /usr/share/nginx/html
