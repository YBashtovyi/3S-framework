FROM node:10 AS prebuild

RUN npm install -g \
   @vue/cli \
   @vue/cli-init \
   @quasar/cli

WORKDIR /app
COPY package*.json ./
RUN npm install

FROM prebuild AS build

ARG ENVIRONMENT_CONFIGURATION
ENV ENVIRONMENT_CONFIGURATION=$ENVIRONMENT_CONFIGURATION

RUN echo "Build environment: $ENVIRONMENT_CONFIGURATION"

WORKDIR /app
COPY . .
RUN quasar build

#FROM registry.gitlab.com/it4medicine-tools/build-tools/projects-basic-image:nginx AS final
FROM harbor.bitsoft.group/build-tools/nginx:1.18 AS final

LABEL maintainer="vbashtovyi@gmail.com"

LABEL description="Frontend for Infrastructure Project Management"

COPY --from=build /app/dist/spa/ /usr/share/nginx/html/

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
