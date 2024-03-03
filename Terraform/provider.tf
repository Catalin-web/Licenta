terraform {
  required_version = "~> 1.7"
  required_providers {
    mycloud = {
      source = "hashicorp/kubernetes"
      version = "~> 1.13"
    }
  }
  backend "local" {
    path = "/tmp/terraform.tfstate"
  }
}

provider "kubernetes" {
  host = "https://192.168.144.109:8443"
}