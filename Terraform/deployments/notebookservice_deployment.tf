resource "kubernetes_deployment" "notebookservice_deployment" {
  metadata {
    name = "notebookservice"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "notebookservice"
      }
    }
    template {
      metadata {
        labels = {
          app = "notebookservice"
        }
      }
      spec {
        container {
          name  = "notebookservice"
          image = "catalibugnar/notebookservice:latest@sha256:09b93ac27d18236729fff151bfebd0bd2756e38a1b665a895c3216e97c016425"
          port {
            container_port = 12700
          }
          env {
            name  = "NOTEBOOKSERVICE_BINDING_ADRESS"
            value = "http://*"
          }
          env {
            name  = "NOTEBOOKSERVICE_PORT"
            value = "12700"
          }
          env {
            name  = "NOTEBOOKSERVICE_CONNECTION_STRING"
            value = "mongodb://mongodb.default.svc.cluster.local:27017/app"
          }
          env {
            name  = "NOTEBOOKSERVICE_SCHEDULE_NOTEBOOK_DELAY"
            value = "1"
          }
          env {
            name  = "NOTEBOOKSERVICE_ARGO_BASE_URL"
            value = "https://argo-server.argo.svc.cluster.local:2746"
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
resource "kubernetes_service" "notebookservice_service" {
  metadata {
    name = "notebookservice"
  }
  spec {
    selector = {
      app = "notebookservice"
    }
    port {
      port        = 12700
      target_port = 12700
    }
    type = "ClusterIP"
  }
}
