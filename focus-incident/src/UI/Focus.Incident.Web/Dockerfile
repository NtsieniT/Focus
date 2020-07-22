FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5031
EXPOSE 8031

FROM microsoft/dotnet:2.1-sdk AS build
# setup proxy
ARG http_proxy
ARG https_proxy
ENV http_proxy=${http_proxy}
ENV https_proxy=${https_proxy}
# get args for the build profile
ARG profile
# setup node & angular-cli
ENV NODE_VERSION 8.11.3
ENV NODE_DOWNLOAD_SHA 1ea408e9a467ed4571730e160993f67a100e8c347f6f9891c9a83350df2bf2be
RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs \
    && npm i -g @angular/cli@6.0.8
WORKDIR /src
COPY src/UI/Focus.Incident.Web/Focus.Incident.Web.csproj src/UI/Focus.Incident.Web/
RUN dotnet restore src/UI/Focus.Incident.Web/Focus.Incident.Web.csproj
COPY . .
WORKDIR /src/src/UI/Focus.Incident.Web
RUN dotnet build --no-restore Focus.Incident.Web.csproj -c $profile -o /app

FROM build AS publish
# get args for the build profile
ARG profile
RUN dotnet publish --no-restore Focus.Incident.Web.csproj -c $profile -o /app

FROM base AS final
# set timezone
ENV TZ=Africa/Johannesburg
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Focus.Incident.Web.dll"]
