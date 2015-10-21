using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CipherSolver.Analysis;
using CipherSolver.Ciphers;
using Microsoft.Win32;

namespace ManualGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DefaultFreqTable.DataContext = CipherSolver.Analysis.Frequency.NATURAL_FREQUENCIES; // Never changed, can be set in initialisation
        }

        /// <summary>
        /// Changes the current displayed element from the drop down list
        /// </summary>
        /// <param name="fullWidthIndex">The index of the element from the drop down list</param>
        private void ChangeDisplay(int fullWidthIndex)
        {
            for (int i = 0; i < 5; i++)
            {
                int value = i == fullWidthIndex ? 1 : 0;
                this.ContentGrid.ColumnDefinitions[i].Width = new GridLength(value, GridUnitType.Star);
            }
        }

        private void Caesar_Selected(object sender, RoutedEventArgs e)
        {
            this.ChangeDisplay(0);
            this.ContentGrid.RowDefinitions[1].Height = new GridLength(60, GridUnitType.Pixel);
            this.CaesarUpDownBox.Value = 3;
        }

        private void Polyalphabetic_Selected(object sender, RoutedEventArgs e)
        {
            this.ChangeDisplay(1);
            this.ContentGrid.RowDefinitions[1].Height = new GridLength(60, GridUnitType.Pixel);
            this.PolyalphabeticKey.Text = string.Empty;
        }

        private void Railfence_Selected(object sender, RoutedEventArgs e)
        {
            this.ChangeDisplay(2);
            this.ContentGrid.RowDefinitions[1].Height = new GridLength(60, GridUnitType.Pixel);
            this.RailfenceUpDownBox.Value = 3;
        }

        private void Transposition_Selected(object sender, RoutedEventArgs e)
        {
            this.ChangeDisplay(3);
            this.ContentGrid.RowDefinitions[1].Height = new GridLength(60, GridUnitType.Pixel);
            this.TranspositionKey.Text = string.Empty;
            this.TranspositionRemoveRepeats.IsChecked = true;
        }

        private void Substitution_Selected(object sender, RoutedEventArgs e)
        {
            this.ChangeDisplay(4);
            this.ContentGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            // Get data
            Dictionary<char, int> textFrequencies = new Dictionary<char, int>();
            List<SubstitutionTemplate> substitutions = new List<SubstitutionTemplate>();
            for (int i = 0; i < 26; i++)
            {
                textFrequencies.Add(Alphabet.LetterAt(i, true), 0);
                substitutions.Add(new SubstitutionTemplate() { Original = Alphabet.LetterAt(i, true), Replace = string.Empty });
            }

            // Bind to data tables
            this.TextFreqTable.DataContext = textFrequencies;
            this.SubstitutionTable.ItemsSource = substitutions;
            this.SubstitutionTable.CanUserAddRows = false;
        }

        private void CaesarDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string cipherText = this.CipherTextBox.Text;
            int shift = (int)this.CaesarUpDownBox.Value;
            string plainText = Caesar.Decrypt(cipherText, shift);
            this.PlainTextBox.Text = plainText;
        }

        private void PolyalphabeticDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string cipherText = this.CipherTextBox.Text;
            string key = this.PolyalphabeticKey.Text;
            string plainText = Polyalphabetic.Decrypt(cipherText, key);
            this.PlainTextBox.Text = plainText;
        }

        private void RailFenceDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string cipherText = this.CipherTextBox.Text;
            int shift = (int)this.RailfenceUpDownBox.Value;
            string plainText = RailFence.Decrypt(cipherText, shift);
            this.PlainTextBox.Text = plainText;
        }

        private void TranspositionDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string cipherText = this.CipherTextBox.Text;
            string key = this.TranspositionKey.Text;
            bool removeRepeats = (bool)this.TranspositionRemoveRepeats.IsChecked;
            string plainText = Transposition.Decrypt(cipherText, key, removeRepeats);
            this.PlainTextBox.Text = plainText;
        }

        private void GetFrequencies_Click(object sender, RoutedEventArgs e)
        {
            this.TextFreqTable.DataContext = Frequency.Analyse(this.CipherTextBox.Text);
        }

        private void Substitute_Click(object sender, RoutedEventArgs e)
        {
            string cipherText = this.CipherTextBox.Text;
            cipherText = cipherText.ToUpper();
            foreach (var item in this.SubstitutionTable.ItemsSource)
            {
                SubstitutionTemplate s = item as SubstitutionTemplate;
                if (s.Replace.Length == 1)
                {
                    cipherText = cipherText.Replace(s.Original, s.Replace.ToLower()[0]);
                }
            }

            this.PlainTextBox.Text = cipherText;
        }

        private void Key_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r = new Regex("[^a-zA-Z]");
            if (r.IsMatch(e.Text))
            {
                MessageBox.Show(this, "Keys can only contain alphabetic letters!");
                e.Handled = true;
            }
        }

        private void Key_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            Regex r = new Regex("[^a-zA-Z]");
            if (r.IsMatch(e.DataObject.GetData(typeof(string)).ToString()))
            {
                MessageBox.Show(this, "Keys can only contain alphabetic letters!");
                e.CancelCommand();
            }
        }

        private void Key_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            int index = t.SelectionStart;
            string text = t.Text;
            string removedSpaces = text.Replace(" ", string.Empty);
            if (text != removedSpaces)
            {
                t.Text = removedSpaces;
                t.SelectionStart = index - 1;
            }
        }

        private void UpperCase_Click(object sender, RoutedEventArgs e)
        {
            CipherTextBox.Text = CipherTextBox.Text.ToUpper();
        }

        private void RemoveSpaces_Click(object sender, RoutedEventArgs e)
        {
            CipherTextBox.Text = CipherTextBox.Text.Replace(" ", string.Empty);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PlainTextBox.Text);
            MessageBox.Show(this, "Copied!");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Create the save dialog
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.OverwritePrompt = true;
            sfd.RestoreDirectory = true;
            sfd.Filter = "Text File|*.txt";
            sfd.Title = "Save decryption";
            sfd.FileName = "Decryption";

            // Open the save dialog
            sfd.ShowDialog(this);

            // Save the file
            if (sfd.FileName != string.Empty)
            {
                // Construct string of data
                string data = string.Format("{0}\r\n\r\nWas decrypted to:\r\n\r\n{1}", CipherTextBox.Text.ToUpper(), PlainTextBox.Text);
                File.WriteAllText(sfd.FileName, data);
                MessageBox.Show(this, "Saved file!");
            }
        }
    }
}
