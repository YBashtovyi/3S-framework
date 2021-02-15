FROM mcr.microsoft.com/dotnet/core/sdk:3.1

LABEL maintainer="angrydevops@gmail.com"
LABEL description="UnitTests for Medical Information System - Marvel"

WORKDIR /UnitTests
COPY . .

RUN apt-get update && apt-get install -y \
    python3 \
    python3-pip \
    && python3 -m pip install requests \
    && python3 -m pip install pytelegrambotapi \
    && cp /usr/share/zoneinfo/Europe/Kiev /etc/localtime \
    && echo "Europe/Kiev" > /etc/timezone
        
# Pull submodules
RUN git submodule sync --recursive && git submodule update --init --recursive

WORKDIR /build/src/App.Business.Tests

RUN unit_tests.py
