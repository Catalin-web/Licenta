resource "helm_release" "jupyterhub" {
  name       = "jupyterhub"
  repository = "https://hub.jupyter.org/helm-chart"
  chart      = "jupyterhub"
  version    = "3.3.7"

  values = [
    file("${path.module}/jupyterhub_values.yaml")
  ]
}
