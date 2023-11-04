using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; 
using System.Text;


//Thisis  only to tests if criptography works with zero width spaces
public class Program
{
  public static void Main()
  {
      string zws = "​"; // Invisible Character (zero width space)
      string normal = "paulo";
      string withZws = "paul​o";

      //Tests to show the strings are different
      if(normal == withZws)
      Console.WriteLine("1true");
      else
      Console.WriteLine("1false​");

      //To show that the zws is not empty
      if(string.IsNullOrEmpty(zws))
      Console.WriteLine("2true");
      else
      Console.WriteLine("2false​");

      //show the invisible string size
      Console.WriteLine(zws.Length);

      string key = "teste"; 

        // Create the encryption class.
        AesEncryption encryption = new AesEncryption(key);               //Using normal string
        AesEncryption encryption2 = new AesEncryption(key + zws);         // Using zero width caractere

        // Encrypt some data.
        string plaintext = "Paulo";
        string ciphertext = encryption.Encrypt(plaintext);
        string ciphertext2 = encryption2.Encrypt(plaintext);

        // Decrypt the data.
        string decryptedText = encryption.Decrypt(ciphertext);
        string decryptedText2 = encryption2.Decrypt(ciphertext2);

        // Print the decrypted data.
        Console.WriteLine(decryptedText);
        Console.WriteLine(ciphertext);
        Console.WriteLine(decryptedText2);
        Console.WriteLine(ciphertext2);
 
  }
 
}


public class AesEncryption
{
    private AesManaged aes;

    private byte[] TruncateHash(byte[] hash, int length)
    {
        Array.Resize(ref hash, length);
        return hash;
    }

    public AesEncryption(string key)
    {
        // Truncate or pad the key to the correct size.
        byte[] keyBytes = TruncateHash(Encoding.UTF8.GetBytes(key), 256 / 8);

        aes = new AesManaged();
        aes.Key = keyBytes;
    }



    public string Encrypt(string plaintext)
    {
        // Convert the plaintext to a byte array.
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        // Create the encryptor.
        var encryptor = aes.CreateEncryptor();

        // Encrypt the data.
        byte[] ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

        // Convert the ciphertext to a string.
        return Convert.ToBase64String(ciphertextBytes);
    }

    public string Decrypt(string ciphertext)
    {
        // Convert the ciphertext to a byte array.
        byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);

        // Create the decryptor.
        var decryptor = aes.CreateDecryptor();

        // Decrypt the data.
        byte[] plaintextBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);

        // Convert the plaintext to a string.
        return Encoding.UTF8.GetString(plaintextBytes);
    }
}


