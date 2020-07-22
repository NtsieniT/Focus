docker-compose -f ..\..\docker-compose.yml -f ..\..\docker-compose.staging.yml build
docker login -u admin -p madness retinadev.intra.absa.co.za:5999
docker push retinadev.intra.absa.co.za:5999/focus_incident_api
docker push retinadev.intra.absa.co.za:5999/focus_incident_web
docker push retinadev.intra.absa.co.za:5999/focus_incident_job