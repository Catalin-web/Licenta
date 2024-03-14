import dataclasses
from models.ModelType import ModelType


@dataclasses.dataclass(frozen=True)
class NotebookParameterToGenerate:
    name_of_the_parameter: str
    description_of_the_parameter: str
    model_type: ModelType

    @staticmethod
    def from_dict(notebook_parameter_dict: dict):
        return NotebookParameterToGenerate(
            name_of_the_parameter=notebook_parameter_dict["nameOfTheParameter"],
            description_of_the_parameter=notebook_parameter_dict[
                "descriptionOfTheParameter"
            ],
            model_type=ModelType(notebook_parameter_dict["modelType"]),
        )

    def to_dict(self):
        return {
            "nameOfTheParameter": self.name_of_the_parameter,
            "descriptionOfTheParameter": self.description_of_the_parameter,
            "modelType": self.model_type.value,
        }
