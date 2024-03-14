resource "helm_release" "jaeger-all-in-one" {
  name       = "jaeger-all-in-one"
  repository = "https://raw.githubusercontent.com/hansehe/jaeger-all-in-one/master/helm/charts"
  chart      = "jaeger-all-in-one"
  version    = "0.1.11"

  values = [
    file("${path.module}/jaeger_values.yaml")
  ]
}
