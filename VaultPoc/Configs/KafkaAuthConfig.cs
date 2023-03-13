using System;

namespace VaultPoc.Configs
{
	public sealed class KafkaAuth
	{
		public string bootstrapServers { get; set; }
		public string saslUsername { get; set; }
		public string saslPassword { get; set; }
	}

	public sealed class SchemaRegistryAuth
	{
		public string basicAuthUserInfo { get; set; }
		public string schemaRegistryUrl { get; set; }
	}

	public sealed class TopicConfig
	{
		public string template { get; set; }
	}

	public class KafkaConfigSection
	{
		public KafkaAuth ClientConfig { get; set; }
		public SchemaRegistryAuth SchemaConfig { get; set; }

		public string clientId { get; set; }
		public TopicConfig topic { get; set; }
	}
}

