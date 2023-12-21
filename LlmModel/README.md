Require file: https://huggingface.co/TheBloke/Llama-2-7B-Chat-GGUF/blob/main/llama-2-7b-chat.Q2_K.gguf downloaded in local 
repository: "./llama-2-7b-chat.Q2_K.gguf"

# Build
docker build . -t llm-model

# Deploy
kubectl apply -f .\k8s\deployment\deployment.yml