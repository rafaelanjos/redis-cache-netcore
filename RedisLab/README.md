#Compila o projeto 'csproj'
dotnet publish -c Release

#Constroi a imagem baseada no DockerFile na pasta atual ( . )
docker build -t redislab .

#Executa o container em modo produtivo
docker run -d -p 81:80 --name rlab redislab 

#Executa o container em modo depuração
docker run -d -p 81:80 -e ASPNETCORE_ENVIRONMENT=Development --name rlab redislab 

#Compose
docker-compose up -d