using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.FinancialBackgroundTasks.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Insightify.FinancialBackgroundTasks.Infrastructure
{
    internal class CryptoDataModelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(MarketValue));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            return new MarketValue { Timestamp = array[0].ToObject<long>(), Value = array[1].ToObject<decimal>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(
                "Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}