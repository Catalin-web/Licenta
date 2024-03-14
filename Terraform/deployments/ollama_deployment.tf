resource "kubernetes_deployment" "ollama" {
  metadata {
    name = "ollama"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "ollama"
      }
    }
    template {
      metadata {
        labels = {
          app = "ollama"
        }
      }
      spec {
        container {
          name  = "ollama"
          image = "ollama/ollama:0.1.29"
          port {
            container_port = 11434
          }
        }
      }
    }
  }
}
// Load balancer:
resource "kubernetes_service" "ollama_service" {
  metadata {
    name = "ollama"
  }
  spec {
    selector = {
      app = "ollama"
    }
    port {
      port        = 11434
      target_port = 11434
    }
    type = "ClusterIP"
  }
}


