FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /app
COPY . .
RUN dotnet publish src/NPost.Services.Deliveries -c release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build /app/src/NPost.Services.Deliveries/out .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT dotnet NPost.Services.Deliveries.dll