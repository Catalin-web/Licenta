resource "kubernetes_deployment" "fileservice" {
    metadata {
        name = "fileservice"
    }
    spec {
        replicas = 1
        selector {
            match_labels = {
                app = "fileservice"
            }
        }
        template {
            metadata {
                labels = {
                    app = "fileservice"
                }
            }
            spec {
                container {
                    name = "fileservice"
                    image = "catalibugnar/fileservice:latest@sha256:64756153ce3574ebbe0128263afa4b6068e21e0a0698a4595a6314521d1312be"
                    port {
                        container_port = 12600
                    }
                    env {
                        name = "FILESERVICE_BINDING_ADRESS"
                        value = "http://*"
                    }
                    env {
                        name = "FILESERVICE_BINDING_PORT"
                        value = "12600"
                    }
                    env {
                        name = "FILESERVICE_MINIO_ENDPOINT"
                        value = "minio.default.svc.cluster.local:9000"
                    }
                    env {
                        name = "FILESERVICE_MINIO_ACCESS_KEY"
                        value = "your_username"
                    }
                    env {
                        name = "FILESERVICE_MINIO_SECRET_KEY"
                        value = "your_password"
                    }
                    env {
                        name = "FILESERVICE_CONNECTION_STRING"
                        value = "mongodb://mongodb.default.svc.cluster.local:27017/app"
                    }
                }
            }
        }
    }
}

