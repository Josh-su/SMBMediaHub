/* JSON ex :
 [
    {
        "Name": "Local Media Folder",
        "IsLocal": true,
        "Path": "C:\\Media\\Movies"
    },
    {
        "Name": "Media Server 1",
        "IsLocal": false,
        "ServerIP": "192.168.1.10",
        "Username": "user1",
        "ShareName": "Movies",
        "EncryptedPassword": "encrypted_password_1"
    },
    {
        "Name": "Media Server 2",
        "IsLocal": false,
        "ServerIP": "192.168.1.15",
        "Username": "user2",
        "ShareName": "TVShows",
        "EncryptedPassword": "encrypted_password_2"
    }
]
 */
using System;
using System.Security.Cryptography;
using System.Text;

namespace SMBMediaHub.JsonClass
{
    internal class Source
    {
        public required string Name { get; set; }

        // Indicates if the source is a local path or SMB share
        public bool IsLocal { get; set; }

        // Path for local sources (only used if IsLocal is true)
        public string? Path { get; set; }

        // SMB-specific properties (used only if IsLocal is false)
        public string? ServerIP { get; set; }
        public string? Username { get; set; }
        public string? ShareName { get; set; }

        // Encrypted password (used only for SMB sources)
        public string? EncryptedPassword { get; set; }

        // Password property to handle encryption/decryption
        public string? Password
        {
            get => EncryptedPassword != null ? DecryptPassword(EncryptedPassword) : null;
            set => EncryptedPassword = value != null ? EncryptPassword(value) : null;
        }

        // Encrypt the password
        private static string EncryptPassword(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        // Decrypt the password
        private static string DecryptPassword(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return string.Empty;

            byte[] encryptedData = Convert.FromBase64String(encryptedText);
            byte[] data = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(data);
        }
    }
}