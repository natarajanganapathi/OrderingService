FROM alpine:latest AS build

RUN apk update \
    && apk add nodejs --no-cache && node -v \
    && apk add npm --no-cache && npm -v 

WORKDIR /opt/ng
COPY package.json package-lock.json ./
ENV NODE_OPTIONS=--max_old_space_size=2048
RUN npm install
 
ENV PATH="./node_modules/.bin:$PATH" 
 
COPY . ./
RUN ng build --prod

FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/nginx.conf
COPY --from=build /opt/ng/dist /usr/share/nginx/html
# RUN addgroup -S <group-name> && adduser -S <user-name> -G <group-name>
# USER <user-name>:<group-name>
RUN addgroup -S svc-group && adduser -S svc-user -G svc-group
USER svc-user:svc-group
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]