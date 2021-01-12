using System;
using System.Web.Mvc;

namespace FileStorage.PL.Helpers
{
    /// <summary>
    /// GetSizeFromBytes, translation number from bytes in readable format
    /// </summary>
    public static class GetSizeFromBytes
    {
        private static readonly string[] suffixes = { "B", "KB", "MB", "GB" };

        public static MvcHtmlString PrintFileSize(int bytes)
        {
            TagBuilder div = new TagBuilder("div");

            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            div.AddCssClass("file-size");
            div.Attributes.Add("title", "Size");
            div.SetInnerText(string.Format("{0:n1}{1}", number, suffixes[counter]));

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString PrintStorageSize(long bytes)
        {
            int counter = 0;
            decimal number = bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return new MvcHtmlString(string.Format("{0:n1}{1}", number, suffixes[counter]));
        }
    }
}