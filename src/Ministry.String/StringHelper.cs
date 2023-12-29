using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Ministry
{
    /// <summary>
    /// Extension method helper for string functions.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "Library")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Library")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Library")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public static class StringHelper
    {
        /// <summary>
        /// Adds spaces into a string where capital letters occur.
        /// </summary>
        /// <param name="value">The string to change.</param>
        /// <returns>The altered string.</returns>
        /// <remarks>As an example TheQuickBrownFox would become The Quick Brown Fox.</remarks>
        public static string AddSpacesByCasing(this string value) 
            => value.AddCharactersByCasing(" ");

        /// <summary>
        /// Adds characters into a string where capital letters occur.
        /// </summary>
        /// <param name="value">The string to change.</param>
        /// <param name="insertCharacters">The characters to insert.</param>
        /// <returns>The altered string.</returns>
        /// <remarks>As an example TheQuickBrownFox would become The|Quick|Brown|Fox, should '|' be used as the insert character.</remarks>
        public static string AddCharactersByCasing(this string value, string insertCharacters)
        {
            var retVal = new StringBuilder();
            foreach (var stringVal in value.Select(c => c.ToString()))
            {
                if (stringVal == stringVal.ToUpper() && retVal.ToString().IsNotNullOrEmpty()) retVal.Append(insertCharacters);
                retVal.Append(stringVal);
            }
            return retVal.ToString();
        }

        /// <summary>
        /// Appends a value to the <see cref="StringBuilder"/> if it is currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="appendValue">The value to append.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendIfEmpty(this StringBuilder value, string appendValue)
        {
            if (value.ThrowIfNull(nameof(value)).Length == 0) value.Append(appendValue.ThrowIfNull(nameof(appendValue)));
            return value;
        }

        /// <summary>
        /// Appends a value to the <see cref="StringBuilder"/> if it is not currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="appendValue">The value to append.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendIfNotEmpty(this StringBuilder value, string appendValue)
        {
            if (value.ThrowIfNull(nameof(value)).Length > 0) value.Append(appendValue.ThrowIfNull(nameof(appendValue)));
            return value;
        }

        /// <summary>
        /// Appends a line terminator after a value to the <see cref="StringBuilder"/> if it is currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="appendValue">The value to append.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendLineIfEmpty(this StringBuilder value, string appendValue)
        {
            if (value.ThrowIfNull(nameof(value)).Length == 0) value.AppendLine(appendValue.ThrowIfNull(nameof(appendValue)));
            return value;
        }

        /// <summary>
        /// Appends line terminator after a value to the <see cref="StringBuilder"/> if it is not currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <param name="appendValue">The value to append.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendLineIfNotEmpty(this StringBuilder value, string appendValue)
        {
            if (value.ThrowIfNull(nameof(value)).Length > 0) value.AppendLine(appendValue.ThrowIfNull(nameof(appendValue)));
            return value;
        }

        /// <summary>
        /// Appends a line terminator to the <see cref="StringBuilder"/> if it is currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendLineIfEmpty(this StringBuilder value)
        {
            if (value.ThrowIfNull(nameof(value)).Length == 0) value.AppendLine();
            return value;
        }

        /// <summary>
        /// Appends line terminator to the <see cref="StringBuilder"/> if it is not currently empty.
        /// </summary>
        /// <param name="value">The <see cref="StringBuilder"/> to append to.</param>
        /// <returns>The completed builder.</returns>
        /// <exception cref="ArgumentNullException">Either the <see cref="StringBuilder"/> or value to append are null.</exception>
        public static StringBuilder AppendLineIfNotEmpty(this StringBuilder value)
        {
            if (value.ThrowIfNull(nameof(value)).Length > 0) value.AppendLine();
            return value;
        }

        /// <summary>
        /// Determines if this instance is Null or Empty.
        /// </summary>
        /// <param name="value">The string to evaluate.</param>
        /// <returns>A flag to indicate the result.</returns>
        /// <remarks>This uses the static <see cref="String.IsNullOrEmpty"/> method.</remarks>
        public static bool IsNullOrEmpty(this string value) 
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Determines if this instance is Null or Empty.
        /// </summary>
        /// <param name="value">The string to evaluate.</param>
        /// <returns>A flag to indicate the result.</returns>
        /// <remarks>This uses the static <see cref="String.IsNullOrEmpty"/> method.</remarks>
        public static bool IsNotNullOrEmpty(this string value) 
            => !value.IsNullOrEmpty();

        /// <summary>
        /// Removes a set number of characters from the end of a string.
        /// </summary>
        /// <param name="value">The string to change.</param>
        /// <param name="characterCount">The number of characters to remove.</param>
        /// <returns>The altered string.</returns>
        /// <exception cref="ArgumentNullException">The string passed to the value parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The characterCount parameter must be non-negative and less than value.Length.</exception>
        public static string RemoveFromEnd(this string value, int characterCount)
        {
            if (characterCount < 0) throw new ArgumentOutOfRangeException(nameof(characterCount));
            if (characterCount > value.Length) throw new ArgumentOutOfRangeException(nameof(characterCount));

            return value.ThrowIfNull(nameof(value)).Remove(value.Length - characterCount, characterCount);
        }

        /// <summary>
        /// Removes a set number of characters from the end of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="value">The StringBuilder to change.</param>
        /// <param name="characterCount">The number of characters to remove.</param>
        /// <returns>The altered StringBuilder.</returns>
        /// <exception cref="ArgumentNullException">The StringBuilder passed to the value parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The characterCount parameter must be non-negative and less than value.Length.</exception>
        public static StringBuilder RemoveFromEnd(this StringBuilder value, int characterCount)
        {
            if (characterCount < 0) throw new ArgumentOutOfRangeException(nameof(characterCount));
            if (characterCount > value.Length) throw new ArgumentOutOfRangeException(nameof(characterCount));

            if (value.ThrowIfNull(nameof(value)).Length > 0)
            {
                return value.Length - characterCount > 0 
                    ? value.Remove(value.Length - characterCount, characterCount) 
                    : new StringBuilder(string.Empty);
            }

            return value;
        }

        /// <summary>
        /// Removes a set number of characters from the start of a string.
        /// </summary>
        /// <param name="value">The string to change.</param>
        /// <param name="characterCount">The number of characters to remove.</param>
        /// <returns>The altered string.</returns>
        /// <exception cref="ArgumentNullException">The string passed to the value parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The characterCount parameter must be non-negative and less than value.Length.</exception>
        public static string RemoveFromStart(this string value, int characterCount) 
            => value.ThrowIfNull(nameof(value)).Remove(0, characterCount);

        /// <summary>
        /// Removes a set number of characters from the start of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="value">The StringBuilder to change.</param>
        /// <param name="characterCount">The number of characters to remove.</param>
        /// <returns>The altered StringBuilder.</returns>
        /// <exception cref="ArgumentNullException">The StringBuilder passed to the value parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The characterCount parameter must be non-negative and less than value.Length.</exception>
        public static StringBuilder RemoveFromStart(this StringBuilder value, int characterCount)
        {
            if (characterCount < 0) throw new ArgumentOutOfRangeException(nameof(characterCount));
            if (characterCount > value.Length) throw new ArgumentOutOfRangeException(nameof(characterCount));

            if (value.ThrowIfNull(nameof(value)).Length > 0)
            {
                return value.Length - characterCount > 0 
                    ? value.Remove(0, characterCount) 
                    : new StringBuilder(string.Empty);
            }
            
            return value;
        }

        /// <summary>
        /// Converts a string to an array.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>An array of strings.</returns>
        /// <exception cref="ArgumentNullException">The string provided to the 'value' parameter is null.</exception>
        /// <exception cref="ArgumentException">The string delimiter provided must be at least 1 character long.</exception>
        public static string[] Split(this string value, string delimiter)
        {
            delimiter.ThrowIfNullOrEmpty(nameof(delimiter));
            var delim = ':';

            if (value.ThrowIfNull(nameof(value)).Contains(delim))
            {
                if (!value.Contains('|'))
                {
                    delim = '|';
                }
                else if (!value.Contains('^'))
                {
                    delim = '^';
                }
                else if (!value.Contains('='))
                {
                    delim = '=';
                }
                else if (!value.Contains('/'))
                {
                    delim = '/';
                }
                else if (!value.Contains('-'))
                {
                    delim = '-';
                }
                else
                {
                    delim = delimiter[0];
                }
            }

            var tmp = value.Replace(delimiter, delim.ToString());
            return tmp.Split(delim);
        }

        /// <summary>
        /// Delimits the specified collection.
        /// </summary>
        /// <param name="col">The collection.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A delimited string representation of the collection.</returns>
        public static string Delimit(this IEnumerable<string> col, string delimiter = ", ")
        {
            var builder = new StringBuilder();

            foreach (var item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendIfNotEmpty(delimiter);
                builder.Append(item);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Delimits the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the value item in the KeyValuePair</typeparam>
        /// <param name="col">The collection.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="keyValueSeparator">The key value separator.</param>
        /// <returns>
        /// A delimited string representation of the collection.
        /// </returns>
        public static string Delimit<T>(this IEnumerable<KeyValuePair<string, T>> col, string delimiter = ", ", string keyValueSeparator = ": ")
        {
            var builder = new StringBuilder();

            foreach (var item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendIfNotEmpty(delimiter);
                builder.Append(item.Key);
                builder.Append(keyValueSeparator);
                builder.Append(item.Value);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a hashtable of strings to a delimited list.
        /// </summary>
        /// <param name="col">The strings to delimit.</param>
        /// <param name="delimiter">The delimiter to split the strings by.</param>
        /// <param name="excludeKeys">A flag to indicate if keys should be excluded.</param>
        /// <param name="keyValueSeparator">The key value separator.</param>
        /// <returns>
        /// The delimited string.
        /// </returns>
        /// <exception cref="ArgumentNullException">The Hashtable passed in is null.</exception>
        public static string Delimit(this IDictionary col, string delimiter = ", ", bool excludeKeys = false, string keyValueSeparator = ": ")
        {
            var builder = new StringBuilder();

            foreach (DictionaryEntry item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendIfNotEmpty(delimiter);
                if (!excludeKeys)
                {
                    builder.Append(item.Key);
                    builder.Append(keyValueSeparator);
                }
                builder.Append(item.Value);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Lists the specified collection.
        /// </summary>
        /// <param name="col">The collection.</param>
        /// <returns>A listed string representation of the collection.</returns>
        public static string List(this IEnumerable<string> col)
        {
            var builder = new StringBuilder();

            foreach (var item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendLineIfNotEmpty();
                builder.Append(item);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Lists the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the value item in the KeyValuePair</typeparam>
        /// <param name="col">The collection.</param>
        /// <param name="keyValueSeparator">The key value separator.</param>
        /// <returns>
        /// A listed string representation of the collection.
        /// </returns>
        public static string List<T>(this IEnumerable<KeyValuePair<string, T>> col, string keyValueSeparator = ": ")
        {
            var builder = new StringBuilder();

            foreach (var item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendLineIfNotEmpty();
                builder.Append(item.Key);
                builder.Append(keyValueSeparator);
                builder.Append(item.Value);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Converts a hashtable of strings to a delimited list.
        /// </summary>
        /// <param name="col">The strings to delimit.</param>
        /// <param name="excludeKeys">A flag to indicate if keys should be excluded.</param>
        /// <param name="keyValueSeparator">The key value separator.</param>
        /// <returns>
        /// The delimited string.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The Hashtable passed in is null.</exception>
        public static string List(this IDictionary col, bool excludeKeys = false, string keyValueSeparator = ": ")
        {
            var builder = new StringBuilder();

            foreach (DictionaryEntry item in col.ThrowIfNull(nameof(col)))
            {
                builder.AppendLineIfNotEmpty();
                if (!excludeKeys)
                {
                    builder.Append(item.Key);
                    builder.Append(keyValueSeparator);
                }
                builder.Append(item.Value);
            }

            return builder.ToString();
        }
    }
}
