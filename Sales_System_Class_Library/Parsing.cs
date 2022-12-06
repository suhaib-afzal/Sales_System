using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Class_Library
{
    public static class Parsing
    {
        /// <summary>
        /// Parses a string representing a uint and gives appropriate 
        /// error messages for the end user. Gives >0 uint.
        /// </summary>
        /// <param name="stringint"> String to be parsed </param>
        /// <param name="inputName"> Name to be given in the error messages</param>
        /// <returns> Returns the >0 uint or 0 when parsing fails </returns>
        public static uint? uintParsing(string stringint, string inputName)
        {
            try
            {
                uint intQuant = uint.Parse(stringint);
                if (intQuant == 0)
                {
                    MessageBox.Show($"{inputName} out of Bounds",
                                    "Parsing Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    return intQuant;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show($"Please input a {inputName}",
                                "Parsing Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return null;
            }
            catch (FormatException)
            {
                MessageBox.Show($"{inputName} cannot be understood",
                                "Parsing Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return null;
            }
            catch (OverflowException)
            {
                MessageBox.Show($"{inputName} out of Bounds",
                                "Parsing Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return null;
            }
        }

        public static string? searchBoxParsing(string? stringInput, string inputName)
        {
            
            bool containsOnlyLettersNumbersAndSpaces = stringInput.ToList().TrueForAll(
                p => Char.IsLetterOrDigit(p) || Char.IsWhiteSpace(p)
                );

            if (!containsOnlyLettersNumbersAndSpaces)
            {
                MessageBox.Show($"{inputName} cannot be understood",
                                "Parsing Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return null;
            }
            else
            {
                return stringInput;
            }
        }
    }
}
