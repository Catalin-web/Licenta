﻿namespace GeneratorService.WebApi.Settings
{
    public class SettingsProviderFactory
    {
        public static ISettingsProvider Create()
        {
            return new EnvironmentVariablesSettingsProvider();
        }
    }
}
