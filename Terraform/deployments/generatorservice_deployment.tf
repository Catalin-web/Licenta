resource "kubernetes_deployment" "generatorservice" {
  metadata {
    name = "generatorservice"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "generatorservice"
      }
    }
    template {
      metadata {
        labels = {
          app = "generatorservice"
        }
      }
      spec {
        container {
          name  = "generatorservice"
          image = "catalibugnar/generatorservice:latest@sha256:5c3395823147ccb3e20dd8013bebee33d2d925bb761403c18fe1b6c81a445f79"
          port {
            container_port = 12800
          }
          env {
            name  = "GENERATORSERVICE_BINDING_ADRESS"
            value = "http://*"
          }
          env {
            name  = "GENERATORSERVICE_PORT"
            value = "12800"
          }
          env {
            name  = "OLLAMA_URL"
            value = "http://ollama.default.svc.cluster.local:11434"
          }
          env {
            name  = "OTEL_URL"
            value = "http://jaeger-all-in-one.default.svc.cluster.local:4317"
          }
        }
      }
    }
  }
}
// Load balancer:
resource "kubernetes_service" "generatorservice_service" {
  metadata {
    name = "generatorservice"
  }
  spec {
    selector = {
      app = "generatorservice"
    }
    port {
      port        = 12800
      target_port = 12800
    }
    type = "ClusterIP"
  }
}


