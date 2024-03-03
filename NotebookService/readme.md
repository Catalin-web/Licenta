# Required steps to run:
0. Set up docker-desktop kubernetes cluster

1. Create namespace "argo"
`kubectl create namespace argo`

2. 1. Install Argo Workflows
`kubectl apply -n argo -f https://github.com/argoproj/argo-workflows/releases/download/v3.5.4/quick-start-minimal.yaml`

2. 2. Test to see it works
`argo submit -n argo --watch https://raw.githubusercontent.com/argoproj/argo-workflows/main/examples/hello-world.yaml`

3. Port forward
`kubectl port-forward svc/argo-server -n argo 2746:2746`

OR:

0. Create:
`./create-argo-containers.ps1`

1. Run:
`./run-argo-server.ps1`

Ui running on: https://localhost:2746/


Deploy:
1. `cd Terraform`
2. `terraform init`
3. `terraform apply`

:)