#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE  5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebScrapper.Api/WebScrapper.Api.csproj", "WebScrapper.Api/"]
RUN dotnet restore "WebScrapper.Api/WebScrapper.Api.csproj"
COPY . .
WORKDIR "/src/WebScrapper.Api"
RUN dotnet build "WebScrapper.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebScrapper.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebScrapper.Api.dll"]