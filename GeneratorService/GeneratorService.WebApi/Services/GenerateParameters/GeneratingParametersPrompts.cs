namespace GeneratorService.WebApi.Services.GenerateParameters
{
    public static class GeneratingParametersPrompts
    {
        public static readonly string SystemPromptToGenerateParameters = "You are an AI assistant that is used to generate some parameters for python functions.";
        public static readonly string UserPromptToGenerateParameters = "The parameter description given by the user is: DESCRIPTION." +
            "The parameter name is NAME and the parameter value is: ";
    }
}
