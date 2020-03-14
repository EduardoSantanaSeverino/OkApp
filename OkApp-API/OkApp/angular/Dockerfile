# stage 1
FROM node:12 as node
WORKDIR /app
COPY . .
RUN yarn install
RUN yarn run build

# stage 2
FROM nginx
COPY --from=node /app/dist /usr/share/nginx/html
COPY fast-nginx-default.conf /etc/nginx/conf.d/default.conf
