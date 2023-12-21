# DEVELOPMENT
# Build
docker build . -t ollama-model
# Run
docker run -d --gpus=all -it -p 11434:11434 --name ollama-model ollama-model
# Pull model locally
docker exec -it ollama-model ollama pull mistral
# Port forward proxy
kubectl port-forward pods/ollama-pod 11434:11434 -n default

# DEPOLYEMNT
# Depoy
kubectl apply -f .\k8s\deployment.yml

# Load model on pod
./load_model_on_pod.ps1