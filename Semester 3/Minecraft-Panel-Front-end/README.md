# minecraft-frontend
The **Minecraft Panel Fronted** is a [Vue.js](https://vuejs.org/) frontend to interact with the [Minecraft Panel API](https://git.fhict.nl/I436237/minecraft-panel-api/).  

## Dependencies

This project relies on the [Minecraft Panel API](https://git.fhict.nl/I436237/minecraft-panel-api/) to handle the requests.  
Please ensure the API project is up and running before attempting to use the **Minecraft Panel Fronted**.  
You can configure the URL's the project will use in `globals/APIConnectionURL.js`.  
Check the Wiki for more information.

## Project setup
```
npm install
yarn install
```

### Compiles and hot-reloads for development
```
npm run serve
yarn run serve
```

### Run with Docker

To run the project in Docker use the command:  
```
docker build -t minecraft-panel-frontend-image . && docker run -p 0.0.0.0:8080:8080 --name minecraft-panel-frontend minecraft-panel-frontend-image
``` 
The application is then available at `http://IP_ADDRESS/`.

### Compiles and minifies for production
```
npm run build
yarn build
```

### Lints and fixes files
```
npm run lint
yarn run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
