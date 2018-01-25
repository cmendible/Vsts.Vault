FROM ubuntu:xenial

RUN apt-get update
RUN apt-get install -y libunwind-dev libcurl3 icu-devtools

# Copy the binaries
COPY /bin/release/netcoreapp2.0/linux-x64/publish app

# Change to app directory
WORKDIR app

# Set ENV variables
ENV VaultConfiguration:TargetFolder /app/backup/
ENV VaultConfiguration:Username vstvault
ENV VaultConfiguration:UserEmail vstvault@vstvault.com

# Start the application using dotnet!!!
CMD ./Vsts.Vault