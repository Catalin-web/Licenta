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
    with open(output_parameters_output_file_path, "w") as f:
        for output_parameter in output_parameters:
            f.write(f"{output_parameter.name}={output_parameter.value}\n")


def get_input_parameter_in_locals(input_parameters_file_path: str, locals: dict):
    with open(input_parameters_file_path, "r") as f:
        for input_parameter_with_equal in f.readlines():
            name, value = input_parameter_with_equal.split("=")
            locals[name] = value


@click.command
# Input parameters
@click.option("--notebook_input_path", type=click.STRING)
@click.option("--input_parameters_file_path", type=click.STRING)
@click.option("--output_parameters_file_path", type=click.STRING)
# Output parameters
@click.option("--output_parameters_output_file_path", type=click.STRING)
def run_notebook(
    notebook_input_path: str,
    input_parameters_file_path: str,
    output_parameters_file_path: str,
    output_parameters_output_file_path: str,
):
    globals, locals = {}, {}
    get_input_parameter_in_locals(input_parameters_file_path, locals)
    execute_notebook_with_globals_and_locals(notebook_input_path, globals, locals)
    output_parameters = get_all_output_parameters(output_parameters_file_path, locals)
    write_output_parameters_to_file(
        output_parameters, output_parameters_output_file_path
    )


if __name__ == "__main__":
    run_notebook()
