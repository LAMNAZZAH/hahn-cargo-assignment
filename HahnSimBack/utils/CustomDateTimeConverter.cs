﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "MM/dd/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();
        return DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
    }
}