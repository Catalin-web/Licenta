from enum import Enum
import dataclasses


@dataclasses.dataclass
class Notebook:
    id: str
    notebook_name: str
    bucket_name: str
    notebook_tags: list[str]

    def from_dict(notebook_with_parameters_and_metadata_dict: dict):
        return Notebook(
            id=notebook_with_parameters_and_metadata_dict["id"],
            notebook_name=notebook_with_parameters_and_metadata_dict["notebookName"],
            bucket_name=notebook_with_parameters_and_metadata_dict["bucketName"],
            notebook_tags=notebook_with_parameters_and_metadata_dict["notebookTags"],
        )
