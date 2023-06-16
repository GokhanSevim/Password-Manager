using Ganss.Xss;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
	public static class Formats
	{
		private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

		public static string ToTitleCase(this string value)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()).Trim();
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return value;
		}

		private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
		{
			try
			{
				return Regex.Replace(stringValue, matchPattern, toReplaceWith);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith, RegexOptions regexOptions)
		{
			try
			{
				return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string Sanitize(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				return new HtmlSanitizer().Sanitize(value).RegexReplace("-{2,}", "-")
						.RegexReplace(@"[*/]+", string.Empty)
						.RegexReplace(@"(<.*?>)", string.Empty)
						.RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string SanitizeUrl(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				return new HtmlSanitizer().Sanitize(value).RegexReplace("-{2,}", "-")
					.RegexReplace(@"(<*)+(>*)", string.Empty)
					.RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string SanitizeLightHTML(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				var sanitizer = new HtmlSanitizer();

				sanitizer.AllowedAttributes.Add("class");
				sanitizer.AllowedAttributes.Add("loading");

				return sanitizer.Sanitize(value, string.Empty).RegexReplace("-{2,}", "-");
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string SanitizeHTML(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				var sanitizer = new HtmlSanitizer();

				sanitizer.AllowedAttributes.Add("class");

				return sanitizer.SanitizeDocument(value, string.Empty).RegexReplace("-{2,}", "-");
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string SanitizeFrame(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				return value.RegexReplace("-{2,}", "-")
					   .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string SanitizePassword(this string value)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
					return string.Empty;

				return value.RegexReplace("-{2,}", "-")
					   .RegexReplace(@"(<*)+(>*)", string.Empty)
					   .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase);
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static string ToEmail(this string value)
		{
			try
			{
				value = value.Sanitize().ToLower();

				if (!string.IsNullOrWhiteSpace(value))
				{
					Encoding iso = Encoding.GetEncoding("ISO-8859-1");
					Encoding utf8 = Encoding.UTF8;
					byte[] utfBytes = utf8.GetBytes(value);
					byte[] isoBytes = Encoding.Convert(utf8, iso, utfBytes);
					string email = iso.GetString(isoBytes).ToUTF8();

					if (email.IsEmail())
					{
						return email;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}

		public static bool IsEmail(this string value)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return false;
		}

		public static string ToUTF8(this string value)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					string result = new Regex("[öÖçÇşŞğĞüÜıİ]").Replace(value, m =>
					{
						return m.Value switch
						{
							"ö" => "o",
							"Ö" => "O",
							"ç" => "c",
							"Ç" => "C",
							"ş" => "s",
							"Ş" => "S",
							"ğ" => "g",
							"Ğ" => "G",
							"ü" => "u",
							"Ü" => "U",
							"ı" => "i",
							"İ" => "I",
							_ => m.Value,
						};
					});
					return result;
				}
			}
			catch (Exception ex)
			{
				_logger.Error(ex);
			}

			return string.Empty;
		}
	}
}
