using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            string sessionData = JsonConvert.SerializeObject(value);
            session.SetString(key, sessionData);

            sessionData = session.GetString(key);
            var cart = JsonConvert.DeserializeObject<Cart>(sessionData);
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T)
                : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}
