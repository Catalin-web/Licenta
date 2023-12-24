# Add repo
helm repo add bitnami https://charts.bitnami.com/bitnami

# Extract values
helm show values bitnami/keycloak > values.yaml

# Create/update 
helm install keycloak bitnami/keycloak --values .\values.yaml
helm upgrade keycloak bitnami/keycloak --values .\values.yaml

# Deploy ingress
kubectl apply -f .\ingress.yaml