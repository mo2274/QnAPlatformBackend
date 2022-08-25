#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QnA Platform Backend/QnA Platform Backend.csproj", "QnA Platform Backend/"]
RUN dotnet restore "QnA Platform Backend/QnA Platform Backend.csproj"
COPY . .
WORKDIR "/src/QnA Platform Backend"
RUN dotnet build "QnA Platform Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QnA Platform Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QnA Platform Backend.dll"]