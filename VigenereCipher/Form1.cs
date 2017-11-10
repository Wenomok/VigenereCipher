//Шифр Виженера

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VigenereCipher
{
    public partial class Form1 : Form
    {
        private static String[,] vigenereSquare;
        private static String[] alphabetEnglish = 
        { "A", "B", "C", "D", "E", "F", "G",
          "H", "I", "J", "K", "L", "M", "N",
          "O", "P", "Q", "R", "S", "T", "U",
          "V", "W", "X", "Y", "Z", " ", ".",
          ",", "!", "?"};

        private static String[] alphabetRussia =
        {   "А", "Б", "В", "Г", "Д", "Е", "Ё",
            "Ж", "З", "И", "Й", "К", "Л", "М",
            "Н", "О", "П", "Р", "С", "Т", "У",
            "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ",
            "Ы", "Ь", "Э", "Ю", "Я", " ", ".",
            ",", "!", "?"};

        List<String> alphabetList;

        private String encryptedText;
        private String reformedText;
        private String reformedSecretWord;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(createVigenereSquare())
            {
                reformedText = text.Text.ToUpper();
                reformedSecretWord = secretWord.Text.ToUpper();

                if (reformedText.Length > 0 && reformedSecretWord.Length > 0)
                {
                    String encryptedString = "";
                    int indexOfSecretWordChar = 0;
                    for (int i = 0; i < reformedText.Length; i++)
                    {
                        if (reformedSecretWord.Length > reformedText.Length)
                        {
                            indexOfSecretWordChar = i % reformedText.Length;
                        }
                        else
                        {
                            indexOfSecretWordChar = i % reformedSecretWord.Length;
                        }


                        encryptedString += vigenereSquare[
                            alphabetList.IndexOf(reformedSecretWord.ElementAt(indexOfSecretWordChar).ToString()),
                            alphabetList.IndexOf(reformedText.ElementAt(i).ToString())];
                    }
                    encryptedText = encryptedString;
                    MessageBox.Show(encryptedText, "Зашифровано", MessageBoxButtons.OK);

                    //EncryptForm encryptForm = new EncryptForm();

                    //encryptForm.receivedText = text.Text;
                    //encryptForm.receivedSecretWord = secretWord.Text;
                    //encryptForm.vigenereSquareEncryptForms = vigenereSquare;

                    //encryptForm.Show();
                }
                else
                {
                    label2.Text = "Требуемые поля необходимо заполнить";
                }
            } else
            {
                label2.Text = "Данный язык не поддерживается";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (text.Text.Length > 0 && secretWord.Text.Length > 0)
            {
                if(encryptedText != null && text.Text.ToUpper().Equals(reformedText))
                {
                    String decryptedString = "";
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        int indexOfSecretWordChar = i % reformedSecretWord.Length;

                        int indexOfCurrentLine = alphabetList.IndexOf(reformedSecretWord.ElementAt(indexOfSecretWordChar).ToString());
                        int indexOfDecryptedChar = 0;

                        for (int j = 0; j < alphabetList.Count; j++)
                        {
                            if (vigenereSquare[indexOfCurrentLine, j].Equals(encryptedText.ElementAt(i).ToString()))
                            {
                                indexOfDecryptedChar = j;
                            }
                        }

                        decryptedString += alphabetList.ElementAt(indexOfDecryptedChar);
                    }
                    MessageBox.Show(decryptedString, "Расшифровано", MessageBoxButtons.OK);
                } else
                {
                    label2.Text = "Для начала зашифруйте текст";
                }
                
            }
            else
            {
                label2.Text = "Требуемые поля необходимо заполнить";
            }
        }

        private bool createVigenereSquare() {
            alphabetList = new List<String>(alphabetEnglish);
            if (alphabetList.Contains(text.Text.ElementAt(0).ToString().ToUpper()))
            {
                setVigenereSquareLanguageWithAlphabet(alphabetEnglish);
                //vigenereSquare = new String[alphabetEnglish.Length, alphabetEnglish.Length];
                //for (int i = 0; i < alphabetEnglish.Length; i++)
                //{
                //    alphabetList = new List<string>(alphabetEnglish);
                //    for (int j = 0; j < alphabetEnglish.Length; j++)
                //    {
                //        vigenereSquare[i, j] = alphabetEnglish[j];
                //    }
                //    alphabetList.Insert(alphabetEnglish.Length, alphabetEnglish.ElementAt(0));
                //    alphabetList.RemoveAt(0);
                //    alphabetEnglish = alphabetList.ToArray();
                //}
                return true;
            }
            else
            {
                alphabetList = new List<string>(alphabetRussia);
                if (alphabetList.Contains(text.Text.ElementAt(0).ToString().ToUpper()))
                {
                    setVigenereSquareLanguageWithAlphabet(alphabetRussia);
                    return true;
                }
            }
            return false;
        }

        private void setVigenereSquareLanguageWithAlphabet(String[] alphabet)
        {
            vigenereSquare = new String[alphabet.Length, alphabet.Length];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabetList = new List<string>(alphabet);
                for (int j = 0; j < alphabet.Length; j++)
                {
                    vigenereSquare[i, j] = alphabet[j];
                }
                alphabetList.Insert(alphabet.Length, alphabet.ElementAt(0));
                alphabetList.RemoveAt(0);
                alphabet = alphabetList.ToArray();
            }
        }
    }
}
