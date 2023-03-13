﻿namespace VaultPoc.Configs
{
	public sealed class ApplicationVaultConfig
	{
		public const string SECTION_NAME = "ApplicationVault";

		public string Address { get; set; }
		public string Namespace { get; set; }
		public string RoleId { get; set; }
		public string RoleSecret { get; set; }
		public string MountPath { get; set; }
	}
}