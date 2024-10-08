Set-Location .\Terraform\
terraform apply --auto-approve

# kubectl port-forward services/userservice 12500:12500 -n default
# Open http://localhost:12500/users/swagger/index.html
Start-Process kubectl -ArgumentList "port-forward services/userservice 12500:12500 -n default"
Start-Process http://localhost:12500/users/swagger/index.html

# kubectl port-forward services/fileservice 12600:12600 -n default
# Open http://localhost:12600/files/swagger/index.html
Start-Process kubectl -ArgumentList "port-forward services/fileservice 12600:12600 -n default"
Start-Process http://localhost:12600/files/swagger/index.html

# kubectl port-forward services/notebookservice 12700:12700 -n default
# Open http://localhost:12700/notebookservice/swagger/index.html
Start-Process kubectl -ArgumentList "port-forward services/notebookservice 12700:12700 -n default"
Start-Process http://localhost:12700/notebookservice/swagger/index.html

# kubectl port-forward services/generatorservice 12800:12800 -n default
# Open http://localhost:12800/generatorservice/swagger/index.html
Start-Process kubectl -ArgumentList "port-forward services/generatorservice 12800:12800 -n default"
Start-Process http://localhost:12800/generatorservice/swagger/index.html

# kubectl port-forward services/argo-server 2746:2746 -n argo
# Open https://localhost:2746
Start-Process kubectl -ArgumentList "port-forward services/argo-server 2746:2746 -n argo"
Start-Process https://localhost:2746

# kubectl --namespace=default port-forward service/proxy-public 8080:http
# Open https://localhost:8080
Start-Process kubectl -ArgumentList "--namespace=default port-forward service/proxy-public 8080:http"
Start-Process http://localhost:8080

Set-Location ..

Set-Location .\NotebookService\
argo template -n argo create .\argo\template.yaml

Set-Location ..