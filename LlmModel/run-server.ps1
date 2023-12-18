$containerCount = docker ps -a -q
if ($containerCount -gt 0) {
    Write-Host "Stopping existing containers"
    docker stop $containerCount
    docker compose down --remove-orphans
}

#docker build --no-cache -t llama-web-api .
docker build -t llama-web-api .
docker run -p 5000:5000 llama-web-api