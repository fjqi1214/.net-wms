using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace BenQGuru.eMES.Common.Helper
{
	/// <summary>
	/// Encryption 的摘要说明。
	/// </summary>
	public class EncryptionHelper
	{
		public static string MD5Encryption(string plainText)
		{	
			return BitConverter.ToString(
				(new MD5CryptoServiceProvider()).ComputeHash(
				Encoding.ASCII.GetBytes(plainText)));
		}

		public static string RSAEncryption(string plainText)
		{			
			return BitConverter.ToString( 
				( new RSACryptoServiceProvider() ).Encrypt(
					Encoding.ASCII.GetBytes( plainText),false ));
		}

		public static string RSADecryption(string encryptedText)
		{
			return BitConverter.ToString( 
				( new RSACryptoServiceProvider() ).Decrypt(
				Encoding.ASCII.GetBytes( encryptedText),false ));
		}

		public static string DESEncryption(string plainText)
		{			
			return _enCrypt(new DESCryptoServiceProvider(),plainText);
		}

		public static string DESDecryption(string encryptedText)
		{
			return _deCrypt(new DESCryptoServiceProvider(),encryptedText);
		}

		public static String EncryptionKey = typeof(System.IO.BinaryReader).ToString() + "-w9" +
			typeof(System.Xml.NameTable).ToString() + "sdf3f" + typeof(Random).ToString() + "jsow23j235ay2s" +
			typeof(EncryptionHelper).ToString() + "a2skwp230a" + typeof(System.Collections.Queue).ToString() + "黶dadjm" +
			typeof(System.NullReferenceException).ToString();

		#region implementation
		private static String _enCrypt(SymmetricAlgorithm Algorithm, String ValueToEnCrypt)
		{
			// 将字符串保存到字节数组中
			Byte [] InputByteArray = Encoding.UTF8.GetBytes(ValueToEnCrypt);

			// 获得需要的密钥
			String EncryptionKey = EncryptionHelper.EncryptionKey;
			
			// 创建一个key.
			Byte [] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
			Algorithm.Key = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.Key.Length);
			Algorithm.IV = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.IV.Length);

			MemoryStream MemStream = new MemoryStream();
			CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
			
			// Write the byte array into the crypto stream( It will end up in the memory stream).
			CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
			CrypStream.FlushFinalBlock();

			// Get the data back from the memory stream, and into a string.
			StringBuilder StringBuilder = new StringBuilder();
			for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
			{
				Byte ActualByte = MemStream.ToArray()[i];
				
				// Format the actual byte as HEX.
				StringBuilder.AppendFormat("{0:X2}", ActualByte);
			}
			
			return StringBuilder.ToString();
		}


		private static String _deCrypt(SymmetricAlgorithm Algorithm, String ValueToDeCrypt)
		{
			// Put the input string into the byte array.
			Byte [] InputByteArray = new Byte[ValueToDeCrypt.Length / 2];

			for (Int32 i = 0; i < ValueToDeCrypt.Length / 2; i++)
			{
				Int32 Value = (Convert.ToInt32(ValueToDeCrypt.Substring(i * 2, 2), 16));
				InputByteArray[i] = (Byte)Value;
			}

			// Create the crypto objects.
			String EncryptionKey = EncryptionHelper.EncryptionKey;
			// Create the key.
			Byte [] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
			Algorithm.Key = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.Key.Length);
			Algorithm.IV = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.IV.Length);
			
			MemoryStream MemStream = new MemoryStream();
			CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
			
			// Flush the data through the crypto stream into the memory stream.
			CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
			CrypStream.FlushFinalBlock();

			// Get the decrypted data back from the memory stream.
			StringBuilder StringBuilder = new StringBuilder();
			
			for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
			{
				StringBuilder.Append((Char)MemStream.ToArray()[i]);
			}

			return StringBuilder.ToString();
		}
		#endregion
	}

	public class ArrayFunctions : Object
	{
		/// <summary>
		/// 重新定义一个数组列表
		/// </summary>
		/// <param name="OriginalArray">需要被重定义的数组</param>
		/// <param name="NewSize">这个数组的新大小</param>
		public static Array ReDim(Array OriginalArray, Int32 NewSize) 
		{
			Type ArrayElementsType = OriginalArray.GetType().GetElementType();
			Array newArray = Array.CreateInstance(ArrayElementsType, NewSize);
			Array.Copy(OriginalArray, 0, newArray, 0, Math.Min(OriginalArray.Length, NewSize));
			return newArray;
		}
	}
}
