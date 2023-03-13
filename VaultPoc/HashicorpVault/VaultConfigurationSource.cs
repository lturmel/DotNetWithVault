using Microsoft.Extensions.Configuration;

namespace VaultPoc.HashicorpVault
{
	public class VaultConfigurationSource : IConfigurationSource
	{
		private VaultOptions _config;

		public VaultConfigurationSource(Action<VaultOptions> config)
		{
			_config = new VaultOptions();
			config.Invoke(_config);
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new VaultConfigurationProvider(_config);
		}
	}
}

