namespace KeyVaultBinding.Config
{
    public interface ICryptoOperations
    {
        byte[] Encrypt(byte[] plain);
        byte[] Decrypt(byte[] cipher);
        byte[] Sign(byte[] digest);
        bool Verify(byte[] digest, byte[] signature);
    }
}