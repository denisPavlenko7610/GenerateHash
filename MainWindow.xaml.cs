using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace HashGeneratorApp
{
    public partial class MainWindow : Window
    {
        const int maxRoundCount = 20;
        public MainWindow()
        {
            InitializeComponent();
            Left = 1825;
            Top = 200;

            GenerateAndDisplayHash();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string randomString = GenerateRandomString();
            string hash = ComputeHash(randomString);
            string truncatedHash = TruncateWithEllipsis(hash, maxRoundCount);
            hashLabel.Content = truncatedHash;
            Clipboard.SetText(hash);
        }

        private string GenerateRandomString()
        {
            const int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var randomString = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                randomString.Append(chars[random.Next(chars.Length)]);
            }

            return randomString.ToString();
        }
        private void GenerateAndDisplayHash()
        {
            string randomString = GenerateRandomString();
            string hash = ComputeHash(randomString);

            string truncatedHash = TruncateWithEllipsis(hash, maxRoundCount);
            hashLabel.Content = truncatedHash;
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string TruncateWithEllipsis(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
                return str;

            int halfMaxLength = maxLength / 2;
            int startLength = halfMaxLength - 1;
            int endLength = halfMaxLength + 1;

            StringBuilder builder = new StringBuilder();
            builder.Append(str.Substring(0, startLength));
            builder.Append(" ..... ");
            builder.Append(str.Substring(str.Length - endLength));

            return builder.ToString();
        }
    }
}
