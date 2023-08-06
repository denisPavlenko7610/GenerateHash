using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace HashGeneratorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Left = 1630;
            Top = 200;

            GenerateAndDisplayHash();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string randomString = GenerateRandomString();
            string hash = ComputeHash(randomString);
            hashLabel.Content = hash;
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
            hashLabel.Content = hash;
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
    }
}
