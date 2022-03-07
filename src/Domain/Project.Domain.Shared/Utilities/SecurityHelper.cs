﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Project.Domain.Shared.Utilities
{
    public static class SecurityHelper
    {
        public static string GetSha256Hash(string input)
        {
            //using (var sha256 = new SHA256CryptoServiceProvider())
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = sha256.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
                //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
            }
        }

        public static string GenerateToken(int byteLength = 32)
        {
            var randomNumber = new byte[byteLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
