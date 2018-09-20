FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 64684 44302

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["./RedisLab.csproj", "RedisLab/"]
RUN dotnet restore "RedisLab/RedisLab.csproj"

WORKDIR "/src/RedisLab"
COPY . .
RUN dotnet build "RedisLab.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RedisLab.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RedisLab.dll"]

#tentativa de subir otimizar o Dfile
#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
#
## create a directory for the app source code 
#RUN mkdir -p /app
#WORKDIR /app
#
## copy the source code
#COPY . /app
#
## Expose the port
#EXPOSE 44302 64684
#
##run the site
#ENTRYPOINT ["dotnet", "RedisLab.dll"]