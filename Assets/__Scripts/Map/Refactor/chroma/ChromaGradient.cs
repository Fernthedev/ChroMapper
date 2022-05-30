using System;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

public class ChromaGradient : ICloneable
{
    [JsonProperty("_duration")]
    [DefaultValue(0)]
    public float Duration;

    [JsonProperty("_startColor")]
    [JsonRequired]
    public Color StartColor;

    [JsonProperty("_endColor")]
    [JsonRequired]
    public Color EndColor;

    [JsonProperty("_easing")]
    [DefaultValue("easeLinear")]
    public string EasingType;

    public ChromaGradient(Color startColor, Color endColor, float duration, string easingType)
    {
        StartColor = startColor;
        EndColor = endColor;
        Duration = duration;
        EasingType = easingType;
    }

    public ChromaGradient Clone() => new ChromaGradient(StartColor, EndColor, Duration, EasingType);

    object ICloneable.Clone() => Clone();
}
