FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

EXPOSE 5000
ENV DOTNET_URLS=http://+:5000

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


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesApp.Api.dll"]
