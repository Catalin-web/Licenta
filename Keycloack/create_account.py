from keycloak import KeycloakOpenID

# Configure client
keycloak_openid = KeycloakOpenID(
    server_url="http://localhost:8080/auth/",
    client_id="account",
    realm_name="Licenta",
    client_secret_key="GKxlmzXliS74l914tn2s4EDCgqW2utYo",
)
