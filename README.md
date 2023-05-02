# cinemaAPI

API is not production ready yet. API uses postgres docker image as development database so to run it locally you need to install [Docker](https://docs.docker.com/get-docker/).

##### Running app locally

1. Clone repository to your local computer and navigate to app's root
2. Start Postgres Docker image
```
docker compose -f docker-compose.dev.yml up
```
3. Inside project root run
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
Endpoints:
* GET /cinemas - to retrieve a list of all cinemas
* GET /cinemas/{id} - to retrieve a specific resource by ID
* GET /cinemas/{id}/showtimes – to get times when shows start
* POST /cinemas - to create a new cinema
* PUT /cinemas/{id} - to update a specific cinema by ID
* DELETE /cinemas/{id} - to delete a specific cinema by ID
