FROM node:23-alpine AS build
COPY package*.json .
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine-slim
COPY --from=build ./dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 81
CMD ["nginx", "-g", "daemon off;"]