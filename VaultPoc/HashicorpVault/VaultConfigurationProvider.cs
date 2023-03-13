
using VaultSharp;
using VaultSharp.V1.AuthMethods.AppRole;

using Microsoft.Extensions.Configuration;

namespace VaultPoc.HashicorpVault
{
	public class VaultConfigurationProvider : ConfigurationProvider
	{
		public VaultOptions _config;
		private IVaultClient _client;

		public VaultConfigurationProvider(VaultOptions config)
		{
			_config = config;
			var vaultClientSettings = new VaultClientSettings(_config.Address, new AppRoleAuthMethodInfo(_config.RoleId, _config.RoleSecret))
			{
				Namespace = _config.Namespace
			};
			_client = new VaultClient(vaultClientSettings);
		}

		public override void Load()
		{
			LoadAsync().Wait();
		}

		public async Task LoadAsync()
		{
			await LoadKafkaConnectionInfo();
		}

		public async Task LoadKafkaConnectionInfo()
		{
			var secret = await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: "kafkaaccess_cacentral01", mountPoint: $"{_config.MountPath}");
			Environment.SetEnvironmentVariable("kafka__clientConfig__bootstrapServers", secret.Data.Data["KAFKA_BOOTSTRAP"].ToString());
            Environment.SetEnvironmentVariable("kafka__clientConfig__saslUsername", secret.Data.Data["KAFKA_SASL_JAAS_USERNAME"].ToString());
            Environment.SetEnvironmentVariable("kafka__clientConfig__saslPassword", secret.Data.Data["KAFKA_SASL_JAAS_PASSWORD"].ToString());
            Environment.SetEnvironmentVariable("kafka__schemaConfig__schemaRegistryUrl", secret.Data.Data["SCHEMA_REGISTRY"].ToString());
			Environment.SetEnvironmentVariable("kafka__schemaConfig__basicAuthUserInfo", $"{secret.Data.Data["SCHEMA_REGISTRY_KEY"]}:{secret.Data.Data["SCHEMA_REGISTRY_SECRET"]}");
        }
	}
}