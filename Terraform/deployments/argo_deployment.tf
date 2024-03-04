resource "null_resource" "apply_argo_workflow" {
  provisioner "local-exec" {
    command = "kubectl apply -n argo -f https://github.com/argoproj/argo-workflows/releases/download/v3.5.4/quick-start-minimal.yaml"
  }
}
