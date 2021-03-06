FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY . .
RUN dotnet restore
WORKDIR ./HotelBooking
ENTRYPOINT ["dotnet", "run"]