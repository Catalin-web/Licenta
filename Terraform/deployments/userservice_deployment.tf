resource "kubernetes_deployment" "userservice_deployment" {
  metadata {
    name = "userservice"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "userservice"
      }
    }
    template {
      metadata {
        labels = {
          app = "userservice"
        }
      }
      spec {
        container {
          name  = "userservice"
          image = "catalibugnar/userservice:latest"
          port {
            container_port = 12500
          }
          env {
            name  = "USERSERVICE_BINDING_ADRESS"
            value = "http://*"
          }
          env {
            name  = "USERSERVICE_PORT"
            value = "12500"
          }
          env {
            name  = "USERSERVICE_CONNECTION_STRING"
            value = "mongodb://mongodb.default.svc.cluster.local:27017/app"
          }
        }
      }
    }
  }
}
// Load balancer:
resource "kubernetes_service" "userservice_service" {
  metadata {
    name = "userservice"
  }
  spec {
    selector = {
      app = "userservice"
    }
    port {
      port        = 12500
      target_port = 12500
    }
    type = "ClusterIP"
  }
}
