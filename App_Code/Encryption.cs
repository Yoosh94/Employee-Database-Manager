/* This code is not mine. I have used Microsoft Virtual Academy Lab https://www.youtube.com/watch?v=LbVRwqrMnZs */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// THis class will handle all the encryption and decryption of all the values. 
/// </summary>
public static class Encryption
{
    public const string PASSWORD = "P@SSW04D";
    static TripleDES CreateDes(string key)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
        des.IV = new byte[des.BlockSize / 8];
        return des;
    }

    public static string EncryptString(string plainText,string password)
    {
        //FIrst we convert the plain text into a byte array
        byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);

        //use a memory stream to hold the bytes
        MemoryStream myStream = new MemoryStream();

        //Create the key and initialization vector using the password
        TripleDES des = CreateDes(password);

        // Create the encoder that will write to the memory stream
        CryptoStream cryptStream = new CryptoStream(myStream, des.CreateEncryptor(), CryptoStreamMode.Write);

        //We now use the crypto stream to write our next byte array to the memory stream
        cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptStream.FlushFinalBlock();

        //Change the encrytped stream to a printable version of out encrypted string
        return Convert.ToBase64String(myStream.ToArray());
    }

    public static string DecryptString(string encryptedText, string password)
    {
        // COnvert our encrypted string to a byte array
        byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

        //Create a memory stream to hold the bytes
        MemoryStream myStream = new MemoryStream();

        // Create the key and initialization vector using the password
        TripleDES des = CreateDes(password);

        //Create our decoder to write to the stream
        CryptoStream decryptStream = new CryptoStream(myStream, des.CreateDecryptor(), CryptoStreamMode.Write);

        //we now use the crypto stream to the byte array
        decryptStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
        decryptStream.FlushFinalBlock();

        //COnvert our stream to a string value
        return Encoding.Unicode.GetString(myStream.ToArray());
    }
}