namespace CarShop.Web.Infrastructure.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private const string CurrencyFormat = "C2";
        private const string NumberFormat = "N0";
        private const string PercentageFormat = "P2";

        public static string ToFriendlyUrl(this string text)
            => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();

        public static string ToDate(this DateTime dateTime)
            => dateTime.ToShortDateString();

        public static string ToNumber(this long number)
            => number.ToString(NumberFormat);

        public static string ToCurrency(this decimal price)
            => price.ToString(CurrencyFormat);

        public static string ToPercentage(this double percentage)
            => $"{percentage}%";
    }
}
