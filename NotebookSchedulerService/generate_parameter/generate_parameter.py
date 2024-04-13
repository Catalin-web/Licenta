import json
import click

from clients.GeneratorServiceClient import GeneratorServiceClient
from models.NotebookParameterToGenerate import NotebookParameterToGenerate


@click.command()
# ENV variables
@click.option("--generator_service_url", type=click.STRING)
# Input parameters
@click.option(
    "--input_parameters_to_generate_file_path",
    type=click.STRING,
)
# Output parameters
@click.option(
    "--input_parameters_to_generate_output_file_path",
    type=click.STRING,
)
@click.option(
    "--has_errors_output_file_path",
    type=click.STRING,
)
@click.option(
    "--error_message_output_file_path",
    type=click.STRING,
)
def generate_parameters(
    generator_service_url: str,
    input_parameters_to_generate_file_path: str,
    input_parameters_to_generate_output_file_path: str,
    has_errors_output_file_path: str,
    error_message_output_file_path: str,
):
    has_error = False
    try:
        generator_service_client = GeneratorServiceClient(generator_service_url)

        parameters_list = []
        with open(input_parameters_to_generate_file_path, "r") as file:
            dict = json.load(file)
            for notebook_parameter_to_generate_dict in dict["inputParametersToGenerate"]:
                notebook_parameter_to_generate = NotebookParameterToGenerate.from_dict(
                    notebook_parameter_to_generate_dict
                )
                parameter_generate_response = generator_service_client.generate_parameter(
                    notebook_parameter_to_generate
                )
                parameters_list.append(
                    {
                        "name": notebook_parameter_to_generate.name_of_the_parameter,
                        "value": parameter_generate_response.value,
                    }
                )

        with open(input_parameters_to_generate_output_file_path, "w") as file:
            json.dump({"parameters": parameters_list}, file)
    except Exception as ex:
        has_error = True
        with open(error_message_output_file_path, "w") as file:
            file.write(str(ex))
    finally:
        with open(has_errors_output_file_path, "w") as file:
            file.write(str(has_error))

if __name__ == "__main__":
    generate_parameters()
