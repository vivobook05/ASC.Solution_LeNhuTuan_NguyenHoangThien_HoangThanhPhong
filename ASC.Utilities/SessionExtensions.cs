using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace ASC.Utilities
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.Set(key, Encoding.UTF8.GetBytes(json));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            if (session.TryGetValue(key, out var data))
            {
                var json = Encoding.UTF8.GetString(data);
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }
    }
}