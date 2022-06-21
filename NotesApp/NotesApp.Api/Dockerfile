FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

#Copy project files
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotesApp.Api/NotesApp.Api.csproj", "NotesApp.Api/"]
COPY ["NotesApp.ApplicationCore/NotesApp.ApplicationCore.csproj", "NotesApp.ApplicationCore/"]
COPY ["NotesApp.Domain/NotesApp.Domain.csproj", "NotesApp.Domain/"]
COPY ["NotesApp.Infrastructure.Data/NotesApp.Infrastructure.Data.csproj", "NotesApp.Infrastructure.Data/"]

RUN dotnet restore "NotesApp.Api/NotesApp.Api.csproj"

#Copy everything else
COPY . .
WORKDIR "/src/NotesApp.Api"
RUN dotnet build "NotesApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotesApp.Api.csproj" -c Release -o /app/publish

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.Api.dll"]
