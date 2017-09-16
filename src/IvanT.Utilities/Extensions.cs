using Newtonsoft.Json;

namespace Utilities
{
    /// <summary>
    /// Utilities extensions. Primary works with objects
    /// </summary>
    public static class Extensions
    {
        // Default serializatio settings for objects
        private static readonly JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        /// Create JSON string from object
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="objectForSerialization">object for serialization</param>
        /// <returns>serialized string</returns>
        public static string ToJsonString<T>(this T objectForSerialization)
        {
            return JsonConvert.SerializeObject(objectForSerialization, DefaultSettings);
        }

        /// <summary>
        /// Create JSON string from object using specific settings
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="objectForSerialization">serialized object</param>
        /// <param name="settings">settings</param>
        /// <returns>result json string</returns>
        public static string ToJsonString<T>(
            this T objectForSerialization,
            JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(objectForSerialization, settings);
        }

        /// <summary>
        /// Deserializa JSON string into object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deserializedString"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string deserializedString)
        {
            return JsonConvert.DeserializeObject<T>(deserializedString, DefaultSettings);
        }

        /// <summary>
        /// Deserializa JSON string into object using specific settings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deserializedString"></param>
        /// <param name="settings">possible serialization settings</param>
        /// <returns></returns>
        public static T ToObject<T>(
            this string deserializedString,
            JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(deserializedString, settings);
        }

        /// <summary>
        /// Create deep clone ob object using Json Serialization 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T CloneJson<T>(this T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };

            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(source), 
                deserializeSettings);
        }
    }
}
