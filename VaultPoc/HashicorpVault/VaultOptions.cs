namespace VaultPoc.HashicorpVault
{
	public sealed class VaultOptions
	{
        public string Address { get; set; }
        public string Namespace { get; set; }
        public string RoleId { get; set; }
        public string RoleSecret { get; set; }
        public string MountPath { get; set; }
    }
}