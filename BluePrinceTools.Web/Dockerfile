FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 5262

ENV ASPNETCORE_URLS=http://+:5262

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
ARG configuration=Release
WORKDIR /src
COPY ["BluePrinceTools.Web/BluePrinceTools.Web.csproj", "BluePrinceTools.Web/"]
RUN dotnet restore "BluePrinceTools.Web/BluePrinceTools.Web.csproj"
COPY . .
WORKDIR "/src/BluePrinceTools.Web"
RUN dotnet build "BluePrinceTools.Web.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BluePrinceTools.Web.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BluePrinceTools.Web.dll"]
