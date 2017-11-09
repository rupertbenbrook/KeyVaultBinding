namespace KeyVaultBinding.Config
{
    public interface IKeyVaultProviderFactory
    {
        IKeyVaultProvider GetKeyVaultProvider(KeyVaultAttribute keyVaultAttribute);
    }
}