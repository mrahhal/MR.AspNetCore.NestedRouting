using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MR.AspNetCore.NestedRouting.Internal
{
	internal static class StringExtensions
	{
		private static readonly char[] Delimeters = { ' ', '-', '_' };

		public static string ReplaceDiacritics(this string @this)
		{
			var formD = @this.Normalize(NormalizationForm.FormD);
			var result = new StringBuilder();

			foreach (var c in formD)
			{
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.EnclosingMark:
						break;

					default:
						result.Append(c);
						break;
				}
			}

			return result.ToString();
		}

		public static string ToAlphaNumeric(this string @this)
		{
			var source = @this.ReplaceDiacritics().ToUpperInvariant();

			const string allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var result = new StringBuilder();

			foreach (var c in source)
			{
				if (allowedChars.Contains(c))
				{
					result.Append(c);
				}
				else
				{
					result.Append('_');
				}
			}

			return result.ToString();
		}

		public static string ToPascalCase(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return SymbolsPipe(
				source,
				'\0',
				(s, i) => new char[] { char.ToUpperInvariant(s) });
		}

		public static string ToCamelCase(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return SymbolsPipe(
				source,
				'\0',
				(s, disableFrontDelimeter) =>
				{
					if (disableFrontDelimeter)
					{
						return new char[] { char.ToLowerInvariant(s) };
					}

					return new char[] { char.ToUpperInvariant(s) };
				});
		}

		public static string ToKebabCase(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return SymbolsPipe(
				source,
				'-',
				(s, disableFrontDelimeter) =>
				{
					if (disableFrontDelimeter)
					{
						return new char[] { char.ToLowerInvariant(s) };
					}

					return new char[] { '-', char.ToLowerInvariant(s) };
				});
		}

		public static string ToSnakeCase(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return SymbolsPipe(
				source,
				'_',
				(s, disableFrontDelimeter) =>
				{
					if (disableFrontDelimeter)
					{
						return new char[] { char.ToLowerInvariant(s) };
					}

					return new char[] { '_', char.ToLowerInvariant(s) };
				});
		}

		public static string ToTrainCase(this string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			return SymbolsPipe(
				source,
				'-',
				(s, disableFrontDelimeter) =>
				{
					if (disableFrontDelimeter)
					{
						return new char[] { char.ToUpperInvariant(s) };
					}

					return new char[] { '-', char.ToUpperInvariant(s) };
				});
		}

		private static string SymbolsPipe(
			string source,
			char mainDelimeter,
			Func<char, bool, char[]> newWordSymbolHandler)
		{
			var builder = new StringBuilder();

			var nextSymbolStartsNewWord = true;
			var disableFrontDelimeter = true;
			for (var i = 0; i < source.Length; i++)
			{
				var symbol = source[i];
				if (Delimeters.Contains(symbol))
				{
					if (symbol == mainDelimeter)
					{
						builder.Append(symbol);
						disableFrontDelimeter = true;
					}

					nextSymbolStartsNewWord = true;
				}
				else if (!char.IsLetterOrDigit(symbol))
				{
					builder.Append(symbol);
					disableFrontDelimeter = true;
					nextSymbolStartsNewWord = true;
				}
				else
				{
					if (nextSymbolStartsNewWord || char.IsUpper(symbol))
					{
						builder.Append(newWordSymbolHandler(symbol, disableFrontDelimeter));
						disableFrontDelimeter = false;
						nextSymbolStartsNewWord = false;
					}
					else
					{
						builder.Append(symbol);
					}
				}
			}

			return builder.ToString();
		}
	}
}
