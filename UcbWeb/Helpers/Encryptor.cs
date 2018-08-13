using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace UcbWeb.Helpers
{
	/// <summary>
	/// Provides methods used to encrpt or decrypt data
	/// </summary>
	public sealed class Encryptor
	{
        /// <summary>
        /// Private constructor to prevent instancing of this class
        /// </summary>
		private Encryptor()
		{}

		/// <summary>
		/// Encrypts or decrypts data based upon encryption password
		/// </summary>
		/// <param name="inputData">string data to be encrypted or decrypted</param>
		/// <param name="password">Password used for encryption</param>
		/// <returns>string containing encrypted or decrypted data</returns>
		public static string Encrypt(string inputData, string password)
		{
			string ReturnValue = "";
			string InputData = inputData;
			int PasswordLength = password.Length;
            char ReadChar, EncryptChar;
			int StartPosition, Position, KeyStream;
			for (int i=0; i<InputData.Length; i++)
			{
				//Get the read position from the given password
				Position = i+1;
				int PositionMod = Position % PasswordLength;
				if(PositionMod== 0)
				{
					StartPosition = PositionMod + PasswordLength;
				}
				else
				{
					StartPosition = PositionMod;
				}
				//Get a char from password based upon the read position
				ReadChar = Convert.ToChar(password.Substring(StartPosition-1, 1));
				//Convert char to ascii value
				KeyStream =  Convert.ToInt32(ReadChar);
				//Get the char from the input data base upon the loop index
				ReadChar = Convert.ToChar(InputData.Substring(i,1));
				//And XORed it with Keystream to encrypt/decrypt the char
                if (ReadChar.ToString().Contains("\t")) ReadChar = Convert.ToChar("\0");
                EncryptChar = Convert.ToChar(Convert.ToInt32(ReadChar) ^ KeyStream);
                if(EncryptChar.ToString().Contains("\0")) EncryptChar = Convert.ToChar("\t");
                
				ReturnValue += EncryptChar;
			}
			return ReturnValue;
		}

        /// <summary>
        /// Creates a key to use for encryption
        /// </summary>
        /// <param name="numberOfBytes">size of key to create</param>
        /// <returns>string containing encryptiion key</returns>
        public static string CreateKey(int numberOfBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[numberOfBytes];
            rng.GetBytes(buffer);
            
            return BytesToHexString(buffer);
        }

        /// <summary>
        /// Converts a byte array to a string the hex equivalent of each byte
        /// </summary>
        /// <param name="bytes">byte array to convert</param>
        /// <returns>string containing hex value of byte array</returns>
        public static string BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(string.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }

        /// <summary>
        /// Converts a byte array to a string the hex equivalent of each byte
        /// </summary>
        /// <param name="bytes">byte array to convert</param>
        /// <returns>string containing hex value of byte array</returns>
        public static byte[] HexStringtoBytes(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new FormatException("hex string has odd number of characters");
            }
            byte[] Bytes = new byte[(hexString.Length) / 2];

            for (int counter = 0; counter < Bytes.Length; counter++)
            {
                 Bytes[counter]= byte.Parse(hexString[counter*2].ToString()+hexString[counter*2+1].ToString(),
                     System.Globalization.NumberStyles.HexNumber);
            }
            return Bytes;
        }

        public static string Crypt(string s_Data, string s_Password, bool b_Encrypt)
        {
            byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };
            PasswordDeriveBytes i_Pass = new PasswordDeriveBytes(s_Password, u8_Salt);
            Rijndael i_Alg = Rijndael.Create();
            i_Alg.Key = i_Pass.GetBytes(32);
            i_Alg.IV = i_Pass.GetBytes(16);
            ICryptoTransform i_Trans = (b_Encrypt) ? i_Alg.CreateEncryptor() : i_Alg.CreateDecryptor();
            MemoryStream i_Mem = new MemoryStream();
            CryptoStream i_Crypt = new CryptoStream(i_Mem, i_Trans, CryptoStreamMode.Write);
            byte[] u8_Data;
            if (b_Encrypt)
            {
                u8_Data = Encoding.Unicode.GetBytes(s_Data);
            }
            else
            {
                u8_Data = Convert.FromBase64String(s_Data);
            }
            try
            {
                i_Crypt.Write(u8_Data, 0, u8_Data.Length);
                i_Crypt.Close();
            }
            catch
            {
                return null;
            }
            if (b_Encrypt)
            {
                return Convert.ToBase64String(i_Mem.ToArray());
            }
            else
            {
                return Encoding.Unicode.GetString(i_Mem.ToArray());
            }
        }

        public static string AssymetricEncrypt(string valueToEncrypt, out RSAParameters encryptionParameters)
        {
            UnicodeEncoding Encoding = new UnicodeEncoding();
            byte[] buffer = Encoding.GetBytes(valueToEncrypt);
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.PersistKeyInCsp = false;
            byte[] outputBuffer = rsa.Encrypt(buffer, true);
            encryptionParameters = rsa.ExportParameters(true);
            return BytesToHexString(outputBuffer);
        }

        public static string AssymetricDecrypt(string valueToDecrypt, RSAParameters encryptionParameters)
        {
            UnicodeEncoding Encoding = new UnicodeEncoding();
            byte[] buffer = HexStringtoBytes(valueToDecrypt);
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();            
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(encryptionParameters);
            byte[] outputBuffer;
            try
            {
                outputBuffer = rsa.Decrypt(buffer, true);
            }
            catch (CryptographicException)
            {
                return null; //DateUtility.FormatDate(DateTime.MinValue);
            }
            return new string(Encoding.GetChars(outputBuffer));
        }
    }
    public static class SafeBase64UrlEncoder
    {
        private const string Plus = "+";
        private const string Minus = "-";
        private const string Slash = "/";
        private const string Underscore = "_";
        private const string EqualSign = "=";
        private const string Pipe = "|";
        private static readonly IDictionary<string, string> _mapper;
        static SafeBase64UrlEncoder()
        {
            _mapper = new Dictionary<string, string> { { Plus, Minus }, { Slash, Underscore }, { EqualSign, Pipe } };
        }
        /// <summary>
        /// Encode the base64 to safeUrl64
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static string EncodeBase64Url(this string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
                return base64Str;
            foreach (var pair in _mapper)
            {
                base64Str = base64Str.Replace(pair.Key, pair.Value);
            }
            return base64Str;
        }
        /// <summary>
        /// Decode the Url back to original base64
        /// </summary>
        /// <param name="safe64Url"></param>
        /// <returns></returns>
        public static string DecodeBase64Url(this string safe64Url)
        {
            if (string.IsNullOrEmpty(safe64Url))
                return safe64Url;
            foreach (var pair in _mapper)
            {
                safe64Url = safe64Url.Replace(pair.Value, pair.Key);
            }
            return safe64Url;
        }
    }
}

