from enum import Enum
import dataclasses


@dataclasses.dataclass(frozen=True)
class NotebookParameter:
    name: str
    value: str

    def to_dict(self):
        return {
            "name": self.name,
            "value": self.value,
        }
