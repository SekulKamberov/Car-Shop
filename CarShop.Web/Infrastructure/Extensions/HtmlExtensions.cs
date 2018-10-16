namespace CarShop.Web.Infrastructure.Extensions
{
    public static class HtmlExtensions
    {
        public static string ToStrongHtml(this string text)
           => $"<strong>{text}</strong>";
    }
}
