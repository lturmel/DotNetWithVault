
using Microsoft.Extensions.Configuration;

using VaultPoc.Configs;
using VaultPoc.HashicorpVault;

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddHashicorpVault()
                .AddEnvironmentVariables()
                .Build();

Console.WriteLine($"Application Name: {configuration.GetValue<string>("ApplicationName")}");

if (configuration.GetSection("ApplicationVault") == null)
{
    throw new ApplicationException("ApplicationVault is missing");
}

Console.WriteLine("");
Console.WriteLine("----- Exposing Confluent Cloud Authentication Configuration -----");
Console.WriteLine("");

var kafkaAuth = new KafkaConfigSection();
configuration.GetSection("kafka").Bind(kafkaAuth);

Console.WriteLine($"kafka.clientConfig.bootstrapServers: {kafkaAuth.ClientConfig.bootstrapServers}");
Console.WriteLine($"kafka.clientConfig.saslUsername: {kafkaAuth.ClientConfig.saslUsername}");
Console.WriteLine($"kafka.clientConfig.saslPassword: {kafkaAuth.ClientConfig.saslPassword}");
Console.WriteLine($"kafka.schemaConfig.schemaRegistryUrl: {kafkaAuth.SchemaConfig.schemaRegistryUrl}");
Console.WriteLine($"kafka.schemaConfig.basicAuthUserInfo: {kafkaAuth.SchemaConfig.basicAuthUserInfo}");
Console.WriteLine($"kafka.clientId: {kafkaAuth.clientId}");
Console.WriteLine($"kafka.topic.template: {kafkaAuth.topic.template}");
