resource "helm_release" "grafana" {
  name       = "grafana"
  repository = "https://grafana.github.io/helm-charts"
  chart      = "grafana"
  version    = "7.3.7"

  values = [
    file("${path.module}/grafana_values.yaml")
  ]
}
