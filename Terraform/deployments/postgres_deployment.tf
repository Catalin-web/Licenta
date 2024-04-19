resource "kubernetes_config_map" "init-sql" {
  metadata {
    name      = "init-sql"
    namespace = "default"
  }

  data = {
    "init.sql" = file("${path.module}/init.sql")
  }
}

resource "kubernetes_deployment" "postgres_deployment" {
  metadata {
    name      = "postgres"
    namespace = "default"
  }
  spec {
    replicas = 1
    selector {
      match_labels = {
        app = "postgres"
      }
    }
    template {
      metadata {
        labels = {
          app = "postgres"
        }
      }
      spec {
        volume {
          name = kubernetes_config_map.init-sql.metadata.0.name
          config_map {
            name = kubernetes_config_map.init-sql.metadata.0.name
          }
        }
        container {
          name  = "postgres"
          image = "postgres:alpine3.19"
          port {
            container_port = 5432
          }
          env {
            name  = "POSTGRES_USER"
            value = "admin"
          }
          env {
            name  = "POSTGRES_PASSWORD"
            value = "admin"
          }
          env {
            name  = "POSTGRES_DB"
            value = "app"
          }
          resources {
            limits = {
              cpu    = "100m"
              memory = "256Mi"
            }
            requests = {
              cpu    = "100m"
              memory = "256Mi"
            }
          }
          volume_mount {
            name       = kubernetes_config_map.init-sql.metadata.0.name
            mount_path = "/docker-entrypoint-initdb.d"
          }
        }
      }
    }
  }
}

// Load balancer:
resource "kubernetes_service" "postgres_service" {
  metadata {
    name = "postgres"
  }
  spec {
    selector = {
      app = "postgres"
    }
    port {
      port        = 5432
      target_port = 5432
    }
    type = "NodePort"
  }
}
