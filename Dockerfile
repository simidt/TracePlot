# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /source
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
RUN npm i -g corepack
WORKDIR /src
# copy csproj and restore as distinct layers
#COPY *.sln .
COPY TracePlot/TracePlot.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY TracePlot/. ./
RUN dotnet publish -c release -o app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src
COPY --from=build /src/app .
ENTRYPOINT ["dotnet", "TracePlot.dll", "--server.urls", "https://+:5000"]