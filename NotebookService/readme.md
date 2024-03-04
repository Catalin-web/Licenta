# Required steps to run:
0. Set up docker-desktop kubernetes cluster

1. Create namespace "argo"
`kubectl create namespace argo`

2. 1. Install Argo Workflows
`kubectl apply -n argo -f https://github.com/argoproj/argo-workflows/releases/download/v3.5.4/quick-start-minimal.yaml`

2. 2. Test to see it works
`argo submit -n argo --watch https://raw.githubusercontent.com/argoproj/argo-workflows/main/examples/hello-world.yaml`

`argo submit -n default --watch ./argo/template.yaml`
`argo submit -n default --watch ./argo/submittable.yaml`

3. Port forward
`kubectl port-forward svc/argo-server 2746:2746`
OR:

0. Create:
`./create-argo-containers.ps1`

1. Run:
`./run-argo-server.ps1`

Ui running on: https://localhost:2746/

# Create/submit

`argo template create .\argo\template.yaml`
`argo submit -n default .\argo\submittable.yaml -p scheduled_notebook_id=65e61f7dd92706945575a3de`

ARGO NAMESPACE:
kubectl port-forward -n argo svc/argo-server 2746:2746

argo template -n argo create .\argo\template.yaml
argo submit -n argo --watch .\argo\submittable.yaml -p scheduled_notebook_id=65e62805012c68e7c67c6ade