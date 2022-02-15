
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TracePlot.Models
{
    /// <summary>
    /// Class <c>TraceRouteConfig</c> describes the format of the JSON input supplied with POST requests to the traceroute endpoint.
    /// </summary>
    public class TraceRouteConfig
    {
        public string Hostname { get; set; }

        [JsonConverter(typeof(EmptyStringJsonConverter))]
        public int NumberOfIterations { get; set; }

        [JsonConverter(typeof(EmptyStringJsonConverter))]
        public int IntervalSize { get; set; }
    }

    /// <summary>
    /// Class <c>EmptyStringJsonConverter</c> is used to correctly parse empty strings in the JSON input.
    /// If an empty string is supplied the converter returns 0.
    /// </summary>
    public class EmptyStringJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (int.TryParse(value, out int result))
                {
                    return result;
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32();
            }

            // Use zero as default value
            return 0;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}