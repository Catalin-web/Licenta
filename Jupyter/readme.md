# Build notebook image
docker build .\JupyterNotebookImage\ -t jupyter-notebook-image

# Build proxy
docker build .\JupyterProxy\ -t jupyter-proxy

# Build ui
docker build .\JupyterUi\ -t jupyter-ui

# Release

helm repo add jupyterhub https://hub.jupyter.org/helm-chart/
helm repo update

helm upgrade --cleanup-on-fail --install jupyterhub/jupyterhub --namespace default --values values.yml

helm show values jupyterhub/jupyterhub > tmp.yml

# Cmd:
helm install jupyterhub jupyterhub/jupyterhub --values .\values.yaml
helm upgrade jupyterhub jupyterhub/jupyterhub --values .\values.yaml

# Port forward by public proxy:
kubectl port-forward services/proxy-public 8000:80 -n default

App running on http://localhost://8000

deploy nginx:
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml

# Apply ingress:
kubectl apply -f ./ingress.yaml