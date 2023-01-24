FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/FizzBuzz/FizzBuzz.API/FizzBuzz.API.csproj", "src/Services/FizzBuzz/FizzBuzz.API/"]
COPY ["src/Services/FizzBuzz/FizzBuzz.Application/FizzBuzz.Application.csproj", "src/Services/FizzBuzz/FizzBuzz.Application/"]
COPY ["src/Services/FizzBuzz/FizzBuzz.Domain/FizzBuzz.Domain.csproj", "src/Services/FizzBuzz/FizzBuzz.Domain/"]
COPY ["src/Services/FizzBuzz/FizzBuzz.Infrastructure/FizzBuzz.Infrastructure.csproj", "src/Services/FizzBuzz/FizzBuzz.Infrastructure/"]
RUN dotnet restore "src/Services/FizzBuzz/FizzBuzz.API/FizzBuzz.API.csproj"
COPY . .
WORKDIR "/src/src/Services/FizzBuzz/FizzBuzz.API"
RUN dotnet build "FizzBuzz.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FizzBuzz.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FizzBuzz.API.dll"]
