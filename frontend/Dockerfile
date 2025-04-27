FROM node:23 AS build

RUN apt-get update \
     && apt-get install default-jre -y \
     && apt-get install default-jdk -y

COPY package*.json .
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine-slim
COPY --from=build ./dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 8282
CMD ["nginx", "-g", "daemon off;"]