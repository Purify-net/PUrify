using System.Collections.Generic;

namespace PUrify.IntegrationTests.Clients
{
    public static class Util
    {
        public static T Get<T>(this IDictionary<string, object> env, string key)
        {
            object value;
            return env.TryGetValue(key, out value) ? (T)value : default(T);
        }
    }
}