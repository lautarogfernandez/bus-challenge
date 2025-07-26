

Para buildear la imagen, pararse en la carpeta donde esta el Dockerfile y correr:
docker build -t busapp-api .


Para correr la Api, correr:
docker run -d -p 7777:80 --name busapp-api-container busapp-api
