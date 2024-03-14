import dataclasses


@dataclasses.dataclass(frozen=True)
class ParameterGeneratorResponse:
    value: str

    @staticmethod
    def from_dict(parameter_generator_response_dict: dict):
        return ParameterGeneratorResponse(
            value=parameter_generator_response_dict["value"],
        )

    def to_dict(self):
        return {
            "value": self.value,
        }
