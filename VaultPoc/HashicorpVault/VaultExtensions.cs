using Microsoft.Extensions.Configuration;

namespace VaultPoc.HashicorpVault
{
	public static class VaultExtensions
	{
		public static IConfigurationBuilder AddHashicorpVault(this IConfigurationBuilder configurationBuilder)
		{
			var configuration = configurationBuilder.Build();

            var applicationVaultConfig = new VaultPoc.Configs.ApplicationVaultConfig();
            configuration.GetSection(VaultPoc.Configs.ApplicationVaultConfig.SECTION_NAME).Bind(applicationVaultConfig);

			return configurationBuilder.AddHashicorpVault(options =>
			{
				options.Address = applicationVaultConfig.Address;
				options.Namespace = applicationVaultConfig.Namespace; 
				options.RoleId = applicationVaultConfig.RoleId;
				options.RoleSecret = applicationVaultConfig.RoleSecret;
				options.MountPath = applicationVaultConfig.MountPath;
			});
		}

		private static IConfigurationBuilder AddHashicorpVault(this IConfigurationBuilder configurationBuilder, Action<VaultOptions> options)
		{
			var vaultOptions = new VaultConfigurationSource(options);
			configurationBuilder.Add(vaultOptions);
			return configurationBuilder;
		}
	}
}