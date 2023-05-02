# cinemaAPI

API is not production ready yet. API uses [postgres docker image](https://hub.docker.com/_/postgres) as development database so to run it locally you need to install [Docker](https://docs.docker.com/get-docker/).

##### Running app locally

1. Clone repository to your local computer and navigate to app's root
2. Start Postgres Docker image (should start to port 5432)
```
docker compose -f docker-compose.dev.yml up
```
It can be shut down with
```
docker compose -f docker-compose.dev.yml down --volumes
```
Image uses volume to persist data (bind to directory postgres-data in local filesystem). This may cause some permission issues.
It's possible to give all users read permissions (Linux) with
```
(sudo) chmod -R a+r postgres-data/
```
Database doesn't contain any initial data.

3. Inside project root run (should start to port 5221)
```
dotnet run
```
API has one simple data model (cinema)
```json
{
  "id": 1
  "name": "string",
  "openingHour": 13
  "closingHour": 21
  "showDuration": 60
}
```
Directory [requests](https://github.com/matiasnisula/cinemaAPI/tree/main/requests) contains some request templates to send http POST, PUT and DELETE requests to the API in VS Code. This requires [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin.

Endpoints:
* GET /cinemas - to retrieve a list of all cinemas
* GET /cinemas/{id} - to retrieve a specific resource by ID
* GET /cinemas/{id}/showtimes – to get times when shows start
* POST /cinemas - to create a new cinema
* PUT /cinemas/{id} - to update a specific cinema by ID
* DELETE /cinemas/{id} - to delete a specific cinema by ID

### TODO
* Add some requirements for Cinema data fields. For example openingHour and closingHour must be between 0 and 24
* Validate user input from POST and PUT requests
* Cinema is fetched by id in multiple endpoints, so maybe some kind of middleware could be implemented
* Give more accurate error messages e.g. when requested data is not found
* Add script for initializing database with some data
