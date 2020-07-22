docker-compose -f ..\..\docker-compose.yml -f ..\..\docker-compose.production.yml build
docker login -u admin -p madness focus11.absa.co.za:5999
docker push focus11.absa.co.za:5999/focus_incident_api 
docker push focus11.absa.co.za:5999/focus_incident_web
docker push focus11.absa.co.za:5999/focus_incident_job