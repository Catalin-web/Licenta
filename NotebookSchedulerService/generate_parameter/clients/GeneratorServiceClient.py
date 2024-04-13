import requests

from models.NotebookParameterToGenerate import NotebookParameterToGenerate
from models.ParameterGeneratorResponse import ParameterGeneratorResponse


class GeneratorServiceClient:
    def __init__(self, generator_service_url: str):
        self.generator_service_url = generator_service_url

    def generate_parameter(
        self, notebook_parameter_to_generate: NotebookParameterToGenerate
    ) -> ParameterGeneratorResponse:
        response = requests.post(
            f"{self.generator_service_url}/generatorService/generate",
            json=notebook_parameter_to_generate.to_dict(),
        )
        response.raise_for_status()
        return ParameterGeneratorResponse.from_dict(response.json())
