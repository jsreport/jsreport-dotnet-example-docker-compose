FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update

# install chrome with deps, see https://github.com/jsreport/jsreport/blob/master/docker/full/Dockerfile
RUN apt-get install -y --no-install-recommends libgconf-2-4 gnupg git curl wget ca-certificates libgconf-2-4 && \
    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
    apt-get update && \  
    apt-get install -y lsb-release google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst --no-install-recommends && \
    wget https://github.com/webnicer/chrome-downloads/raw/master/x64.deb/google-chrome-stable_79.0.3945.130-1_amd64.deb && \
    dpkg -i ./google-chrome*.deb && \
    rm google-chrome*.deb

ENV chrome_launchOptions_executablePath google-chrome-stable
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY "WebApp/WebApp.csproj" "/src/WebApp/WebApp.csproj"
RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp.dll"]