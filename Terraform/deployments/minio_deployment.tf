resource "kubernetes_deployment" "minio" {
    metadata {
        name = "minio"
    }
    spec {
        replicas = 1
        selector {
            match_labels = {
                app = "minio"
            }
        }
        template {
            metadata {
                labels = {
                    app = "minio"
                }
            }
            spec {
                container {
                    name = "minio"
                    image = "docker.io/bitnami/minio:2024.2.26"
                    port {
                        container_port = 9000
                    }
                    env {
                        name = "MINIO_ROOT_USER"
                        value = "your_username"
                    }
                    env {
                        name = "MINIO_ROOT_PASSWORD"
                        value = "your_password"
                    }
                }
            }
        }
    }
}

