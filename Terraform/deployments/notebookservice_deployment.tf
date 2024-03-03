resource "kubernetes_deployment" "notebookservice" {
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
                    name = "notebookservice"
                    image = "catalibugnar/notebookservice:latest@sha256:57caa96c079732795910fd78fd12d27adad2fc16833355c37163abc2df669ad6"
                    port {
                        container_port = 12700
                    }
                    env {
                        name = "NOTEBOOKSERVICE_BINDING_ADRESS"
                        value = "http://*"
                    }
                    env {
                        name = "NOTEBOOKSERVICE_BINDING_PORT"
                        value = "12700"
                    }
                    env {
                        name = "NOTEBOOKSERVICE_CONNECTION_STRING"
                        value = "mongodb://mongodb.default.svc.cluster.local:27017/app"
                    }
                }
            }
        }
    }
}

