# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy the solution and project files (optimizes caching)
COPY NotesApp.sln ./
COPY NotesApp.Api/*.csproj ./NotesApp.Api/
COPY NotesApp.Business/*.csproj ./NotesApp.Business/
COPY NotesApp.Data/*.csproj ./NotesApp.Data/

# restore dependencies
RUN dotnet restore

# copy the rest of the code
COPY ./ ./

# publish the app
WORKDIR /src/NotesApp.Api
RUN dotnet publish -c Release -o /app

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# copy published output from build stage
COPY --from=build /app .

# start the app
ENTRYPOINT ["dotnet", "NotesApp.Api.dll"]