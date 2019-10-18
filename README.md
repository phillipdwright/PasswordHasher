# Password Hasher
This repository houses a .NET Core WebAPI project that can be used to start a server that hashes text using the SHA512 algorithm and returns a base64-encoded representation of the hash.
## Requirements
To run the server locally, you will need the [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) installed.
## Running the server
Run the commands below to start the server.
```
$ git clone https://github.com/phillipdwright/PasswordHasher.git
$ cd PasswordHasher/
$ dotnet run --project PasswordHasher.WebApi/PasswordHasher.WebApi.csproj
```
## Make calls to API endpoints to create hashes
Start a hash job. The response will contain the ID of the job that was started.
```
$ curl --data "password=angryMonkey" http://localhost:8080/hash
1
```
Request the hash from a completed job by referencing the job's ID.
```
$ curl http://localhost:8080/hash/1
ZEHhWB65gUlzdVwtDQArEyx+KVLzp/aTaRaPlBzYRIFj6vjFdqEb0Q5B8zVKCZ0vKbZPZklJz0Fd7su2A+gf7Q==
```
See information about hash requests that have been made, including a total count and the average processing time in milliseconds.
```
$ curl http://localhost:8080/stats
{"total":1,"average":1}
```
Stop the server. (Note that all pending hash jobs will process before the server shuts down, but no new jobs can be started after shutdown is initiated.)
```
$ curl --data {} http://localhost:8080/shutdown
$
```
## Run the unit tests
Run the commands below to find all unit test projects in the repository (in folders ending in `Tests`) and to run the unit tests within them.
```
$ find ./*Tests -name '*.csproj' -print0 | xargs -L1 -0 dotnet test
```
