using Microsoft.AspNetCore.Rewrite;
using static System.Net.Mime.MediaTypeNames;

namespace ExpenseTracker.UserService.Api.Common
{
    public static class SvgAvatarGenerator
    {
        private static readonly string[] Colors =
          [
              "#1abc9c", "#2ecc71", "#3498db", "#9b59b6", "#34495e",
                    "#16a085", "#27ae60", "#2980b9", "#8e44ad", "#2c3e50",
                    "#f39c12", "#e67e22", "#e74c3c", "#d35400", "#c0392b"
          ];

        public static string CreatePlaceholderProfile(string name , int size=128)
        {
            string svg = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                svg = "ET";
                name = "ET";
            }
            else
            {
                var parts = name.Split(' ');
                if (parts.Length == 1)
                {
                    svg = $"{char.ToUpper(parts[0][0])}{char.ToUpper(parts[0][1])}";
                }
                else
                {
                    svg = $"{char.ToUpper(parts[0][0])}{char.ToUpper(parts[1][0])}";
                }
            }

            int hash = Math.Abs(name.Trim().ToLowerInvariant().GetHashCode());
            var colhash = Colors[hash % Colors.Length];
            int fontsize = size / 2;

            string uri = $"""<svg xmlns="http://www.w3.org/2000/svg" width="{size}" height="{size}" viewBox="0 0 {size} {size}"><circle cx="{fontsize}" cy="{fontsize}" r="{fontsize}" fill="{colhash}"/><text x="50%" y="50%" font-family="system-ui, -apple-system, sans-serif" font-size="{fontsize}px" font-weight="bold" fill="#FFFFFF" text-anchor="middle" dominant-baseline="central">{svg}</text></svg>""";

            return $"data:image/svg+xml;utf8,{Uri.EscapeDataString(uri)}";

        }
    }
}
