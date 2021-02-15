# Quasar App

# Setting up local environment

## Local development requires

- Node.js >= 8.9.0 is required.
- Vue
- Quasar

## 1. Install Vue

```
npm install -g @vue/cli @vue/cli-init
```

## 2. Install & update Quasar

```
npm uninstall -g quasar-cli
npm install -g @quasar/cli
npm install yarn -g

yarn add --dev @quasar/app
yarn add quasar @quasar/extras
```

## 3. Install required node modules

```
yarn install (or npm install)
```

## 4. Start local development server

```
quasar dev
```

## 4. Build dist

```
quasar build
```
## 5. Creating deploy files for different environment (with cmd file)
## 5.1 Set default configuration

```
quasar-build.cmd 
```
## 5.2 Set DEV, TEST and PROD environments

```
quasar-build.cmd dev
quasar-build.cmd test
quasar-build.cmd prod
```
