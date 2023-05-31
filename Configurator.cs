using System;
using System.Collections.Generic;
using System.IO;

public sealed class Configurator
{
    private static readonly Lazy<Configurator> instance = new Lazy<Configurator>(() => new Configurator());
    private readonly Dictionary<string, Dictionary<string, string>> configValues;
    private readonly string configFile = "Config.ini";
    private Configurator()
    {
        configValues = LoadConfigValues();
    }

    public static Configurator Instance => instance.Value;

    public static string Read(string section, string key)
    {
        if (Instance.configValues.ContainsKey(section) && Instance.configValues[section].ContainsKey(key))
        {
            return Instance.configValues[section][key];
        }
        return null; // Or throw an exception if the section or key is required
    }

    public static void Update(string section, string key, string value)
    {
        if (!Instance.configValues.ContainsKey(section) || !Instance.configValues[section].ContainsKey(key))
        {
            throw new ArgumentException("Invalid section or key.");
        }

        Instance.configValues[section][key] = value;

        // Update the value in the config file
        UpdateConfigFile();
    }

    public static void Add(string section, string key, string value)
    {
        if (!Instance.configValues.ContainsKey(section))
        {
            Instance.configValues[section] = new Dictionary<string, string>();
        }

        Instance.configValues[section][key] = value;

        // Update the value in the config file
        UpdateConfigFile();
    }

    private static void UpdateConfigFile()
    {
        using (StreamWriter writer = new StreamWriter(Instance.configFile))
        {
            foreach (var section in Instance.configValues.Keys)
            {
                writer.WriteLine($"[{section}]");

                foreach (var keyValue in Instance.configValues[section])
                {
                    writer.WriteLine($"{keyValue.Key}={keyValue.Value}");
                }

                writer.WriteLine();
            }
        }
    }

    private Dictionary<string, Dictionary<string, string>> LoadConfigValues()
    {
        Dictionary<string, Dictionary<string, string>> values = new Dictionary<string, Dictionary<string, string>>();

        if (File.Exists(configFile))
        {
            string[] lines = File.ReadAllLines(configFile);
            string currentSection = null;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    // Section line
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    values[currentSection] = new Dictionary<string, string>();
                }
                else if (!string.IsNullOrEmpty(trimmedLine) && !trimmedLine.StartsWith(";"))
                {
                    // Key-value line
                    int equalsIndex = trimmedLine.IndexOf('=');
                    if (equalsIndex >= 0)
                    {
                        string key = trimmedLine.Substring(0, equalsIndex).Trim();
                        string value = trimmedLine.Substring(equalsIndex + 1).Trim();

                        if (!string.IsNullOrEmpty(currentSection))
                        {
                            values[currentSection][key] = value;
                        }
                    }
                }
            }
        }

        return values;
    }
}
