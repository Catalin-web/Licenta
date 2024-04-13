import json
import click
from json import load

from models.NotebookParameter import NotebookParameter


def execute_notebook_with_globals_and_locals(
    notebook_input_path: str, globals: dict, locals: dict
):
    with open(notebook_input_path) as f:
        notebook_as_json = load(f)
    for cell in notebook_as_json["cells"]:
        if cell["cell_type"] == "code":
            source = "".join(
                line for line in cell["source"] if not line.startswith("%")
            )
            exec(source, globals, locals)


def get_all_output_parameters(
    output_parameters_file_path: str, locals: dict
) -> list[NotebookParameter]:
    output_parameters: list[NotebookParameter] = []
    with open(output_parameters_file_path, "r") as f:
        for output_parameter_name in f.readlines():
            output_parameter_name = output_parameter_name.strip()
            output_parameter_value = locals[output_parameter_name]
            output_parameters.append(
                NotebookParameter(
                    name=output_parameter_name,
                    value=output_parameter_value,
                )
            )
    return output_parameters


def write_output_parameters_to_file(
    output_parameters: list[NotebookParameter], output_parameters_output_file_path: str
):
    parameters_list = []
    with open(output_parameters_output_file_path, "w") as f:
        for output_parameter in output_parameters:
            parameters_list.append(output_parameter.to_dict())
        json.dump({"output_parameters": parameters_list}, f)


def get_input_parameter_in_locals(input_parameters_file_path: str, locals: dict):
    with open(input_parameters_file_path, "r") as f:
        dict = json.load(f)
        for input_parameter in dict["parameters"]:
            name, value = input_parameter["name"], input_parameter["value"]
            locals[name] = value


def get_input_generated_parameter_in_locals(
    input_parameters_to_generate_output: str, locals: dict
):
    with open(input_parameters_to_generate_output, "r") as f:
        dict = json.load(f)
        for generated_parameter in dict["parameters"]:
            name, value = generated_parameter["name"], generated_parameter["value"]
            locals[name] = value


@click.command
# Input parameters
@click.option("--notebook_input_path", type=click.STRING)
@click.option("--input_parameters_file_path", type=click.STRING)
@click.option("--input_parameters_to_generate_output", type=click.STRING)
@click.option("--output_parameters_file_path", type=click.STRING)
# Output parameters
@click.option("--output_parameters_output_file_path", type=click.STRING)
@click.option(
    "--has_errors_output_file_path",
    type=click.STRING,
)
@click.option(
    "--error_message_output_file_path",
    type=click.STRING,
)
def run_notebook(
    notebook_input_path: str,
    input_parameters_file_path: str,
    input_parameters_to_generate_output: str,
    output_parameters_file_path: str,
    output_parameters_output_file_path: str,
    has_errors_output_file_path: str,
    error_message_output_file_path: str,
):
    has_error = False
    try:
        globals, locals = {}, {}
        get_input_parameter_in_locals(input_parameters_file_path, locals)
        get_input_generated_parameter_in_locals(input_parameters_to_generate_output, locals)
        execute_notebook_with_globals_and_locals(notebook_input_path, globals, locals)
        output_parameters = get_all_output_parameters(output_parameters_file_path, locals)
        write_output_parameters_to_file(
            output_parameters, output_parameters_output_file_path
        )
    except Exception as ex:
        has_error = True
        with open(error_message_output_file_path, "w") as file:
            file.write(str(ex))
    finally:
        with open(has_errors_output_file_path, "w") as file:
            file.write(str(has_error))



if __name__ == "__main__":
    run_notebook()
