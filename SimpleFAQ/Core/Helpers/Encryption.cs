using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SimpleFAQ.Core.Helpers
{
	/// <summary>
	/// Encryption helpers.
	/// </summary>
	public static class Encryption
	{
		/// <summary>
		/// Computes a salted string using the specified hasing algorithm.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="algorithm"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string ComputerHash(string input, HashAlgorithm algorithm, byte[] salt)
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(input);

			byte[] saltedInput = new byte[salt.Length + inputBytes.Length];
			inputBytes.CopyTo(saltedInput, salt.Length);

			byte[] hashedBytes = algorithm.ComputeHash(saltedInput);

			return BitConverter.ToString(hashedBytes).Replace("-", "");
		}
	}
}