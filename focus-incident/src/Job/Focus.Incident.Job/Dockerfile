FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
## setup proxy
ARG http_proxy
ARG https_proxy
ENV http_proxy=${http_proxy}
ENV https_proxy=${https_proxy}
## get args for the build profile
ARG profile
WORKDIR /src
COPY src/Job/Focus.Incident.Job/Focus.Incident.Job.csproj src/Job/Focus.Incident.Job/
COPY src/Infrastructure/Focus.Incident.Infrastructure.DB/Focus.Incident.Infrastructure.DB.csproj src/Infrastructure/Focus.Incident.Infrastructure.DB/
COPY src/Domain/Focus.Incident.Domain/Focus.Incident.Domain.csproj src/Domain/Focus.Incident.Domain/
RUN dotnet restore src/Job/Focus.Incident.Job/Focus.Incident.Job.csproj
COPY . .
WORKDIR /src/src/Job/Focus.Incident.Job
RUN dotnet build --no-restore Focus.Incident.Job.csproj -c $profile -o /app

FROM build AS publish
## get args for the build profile
ARG profile
RUN dotnet publish --no-restore Focus.Incident.Job.csproj -c $profile -o /app

FROM base AS final
# set timezone
ENV TZ=Africa/Johannesburg
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Focus.Incident.Job.dll"]
