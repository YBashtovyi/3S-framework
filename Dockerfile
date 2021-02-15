#FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
COPY . .

# Pull submodules
CMD git submodule sync --recursive
RUN git submodule sync --recursive && git submodule update --init --recursive

WORKDIR /app/src/App.Api

RUN dotnet restore \
    && dotnet build \
    && dotnet publish -c Release -o output

#FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

LABEL maintainer="..."
LABEL description="..."

WORKDIR /app

ARG VERSION_VALUE
ENV VERSION_VALUE=$VERSION_VALUE

# Set timezone UTC +2
ENV TZ=Europe/Kiev

COPY --from=build-env /app/src/App.Api/output .
COPY --from=build-env /app/src/App.Data/Migrations/Sql ./sql
COPY --from=build-env /app/xmldocumentation ./xmldocumentation
#COPY --from=build-env /app/submodules/App.Core/xmldocumentation ./xmldocumentation

RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime \
    && echo $TZ > /etc/timezone

ENTRYPOINT ["dotnet", "App.WebAPI.dll", "--urls", "http://0.0.0.0:5051"]
