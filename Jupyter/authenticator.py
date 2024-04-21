from jupyterhub.auth import Authenticator


class MyCustomAuthenticator(Authenticator):
    auto_login = True

    async def authenticate(self, handler, data):
        return "user"
