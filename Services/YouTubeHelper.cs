using System.Text.RegularExpressions;

namespace StoriesOfInclusion.Services
{
    public static class YouTubeHelper
    {
        public static string GetEmbedUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "https://www.youtube.com/embed/0bT-30FkF6E?autoplay=1";

            url = url.Trim();

            // Already embed URL
            if (url.Contains("youtube.com/embed/"))
            {
                return url.Contains("?") ? $"{url}&autoplay=1" : $"{url}?autoplay=1";
            }

            // youtu.be/{id}
            var youtuMatch = Regex.Match(url, @"youtu\.be\/([a-zA-Z0-9_-]{11})");
            if (youtuMatch.Success)
            {
                return $"https://www.youtube.com/embed/{youtuMatch.Groups[1].Value}?autoplay=1";
            }

            // youtube.com/watch?v={id} or /shorts/{id}
            var watchMatch = Regex.Match(url, @"(?:v=|\/v\/|embed\/|shorts\/)([a-zA-Z0-9_-]{11})");
            if (watchMatch.Success)
            {
                return $"https://www.youtube.com/embed/{watchMatch.Groups[1].Value}?autoplay=1";
            }

            // Raw 11-char ID
            if (url.Length == 11 && !url.Contains("/") && !url.Contains("."))
            {
                return $"https://www.youtube.com/embed/{url}?autoplay=1";
            }

            // Fallback default
            return "https://www.youtube.com/embed/0bT-30FkF6E?autoplay=1";
        }
    }
}
