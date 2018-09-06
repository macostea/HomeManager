docker build -t mcostea/db-save-func -f Functions/SaveToDB/Dockerfile .
docker push mcostea/db-save-func
faas-cli --gateway=http://172.16.254.142:8080 deploy --yaml=Functions/SaveToDB/function.yml

