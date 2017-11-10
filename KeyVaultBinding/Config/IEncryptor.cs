namespace KeyVaultBinding.Config
{
    public interface IEncryptor
    {
        byte[] Encrypt(byte[] plainBytes);
        byte[] Decrypt(byte[] cipherBytes);
    }
}