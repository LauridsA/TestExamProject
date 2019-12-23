FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers -- not too clean, not very extendable
COPY DataAccess/*.csproj DataAccess/
COPY HotelBooking/*.csproj HotelBooking/
COPY Models/*.csproj Models/
COPY Services/*.csproj Services/
RUN dotnet restore HotelBooking/HotelBooking.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/HotelBooking/out .
ENTRYPOINT ["dotnet", "HotelBooking/bin/Debug/netcoreapp2.2/HotelBooking.dll"]