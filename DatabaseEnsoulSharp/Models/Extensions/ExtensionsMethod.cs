using DatabaseEnsoulSharp.Models.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DatabaseEnsoulSharp.Models.Extensions
{
    public static class ExtensionsMethod
    {
        public static ScriptInfo ScriptInfoExtension(this ChampionScript championScript, List<ScriptInfo> scriptInfo)
        {
            return scriptInfo.FirstOrDefault(a => a.Id == championScript.IdScriptInfo);
        }

        public static Champion ChampionExtension(this ChampionScript championScript, List<Champion> champion)
        {
            return champion.FirstOrDefault(a => a.Id == championScript.IdChampion);
        }

        public static void SetCookie(this HttpResponse response, string key, string value, double? time = null)
        {
            var option = new CookieOptions
            {
                Expires = time.HasValue
                    ? DateTime.Now.AddDays(time.Value)
                    : DateTime.Now.AddDays(1)
            };
            response.Cookies.Append(key, value, option);
        }

        public static void RemoveCookie(this HttpResponse response, string key)
        {
            response.Cookies.Delete(key);
        }

        public static string ToJson(this object obj)
        {
            return obj != null ? JsonConvert.SerializeObject(obj) : null;
        }

        public static T ToObject<T>(this string text) where T : class
        {
            return text == null ? null : JsonConvert.DeserializeObject<T>(text);
        }
        public static void Set(this ISession session, string key, string value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static string Get(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value;
        }
    }
}