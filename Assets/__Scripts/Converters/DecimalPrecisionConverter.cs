using System;
using Newtonsoft.Json;

namespace __Scripts.Converters
{
    public class DecimalPrecisionConverter : JsonConverter<float>
    {
        // TODO: Configurable?
        private static int DecimalPrecision => Settings.Instance.TimeValueDecimalPrecision;
        
        public override void WriteJson(JsonWriter writer, float value, JsonSerializer serializer) => writer.WriteValue(Math.Round((decimal) value, DecimalPrecision));

        public override float ReadJson(JsonReader reader, Type objectType, float existingValue, bool hasExistingValue,
            JsonSerializer serializer) =>
            (float)reader.ReadAsDouble();
    }
}
