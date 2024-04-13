resource "kubernetes_deployment" "mongodb_deployment" {
    metadata {
        name = "mongodb"
    }
    spec {
        replicas = 1
        selector {
            match_labels = {
                app = "mongodb"
            }
        }
        template {
            metadata {
                labels = {
                    app = "mongodb"
                }
            }
            spec {
                container {
                    name = "mongodb"
                    image = "bitnami/mongodb:7.0.2"
                    port {
                        container_port = 27017
                    }
                    env {
                        name = "MONGO_INITDB_DATABASE"
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
                }
            }
        }
    }
}
// Load balancer:
resource "kubernetes_service" "mongodb_service" {
    metadata {
        name = "mongodb"
    }
    spec {
        selector = {
            app = "mongodb"
        }
        port {
            port = 27017
            target_port = 27017
        }
        type = "NodePort"
    }
}