
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2CustomEvent : ICustomEvent
{
    private V2CustomEvent(IDictionary<string,JToken> unserializedData) => UnserializedData = unserializedData;

    [JsonConstructor]
    public V2CustomEvent(IDictionary<string, JToken> unserializedData, [CanBeNull] V2CustomEventCustomData customData)
    {
        UnserializedData = unserializedData;
        CustomData = customData;
    }

    [JsonIgnore] public bool isV3 => false;
    public IBeatmapJSON Clone() => new V2CustomEvent(new Dictionary<string, JToken>(UnserializedData));

    [JsonExtensionData] 
    public IDictionary<string, JToken> UnserializedData { get; set; }


    [JsonProperty("_data")]
    public ICustomData CustomData
    {
        get;
        set;
    }

    [JsonProperty("_time")]
    public float Time
    {
        get => UnserializedData["_time"].ToObject<float>();
        set => UnserializedData["_time"] = value;
    }

    [JsonProperty("_type")]
    public string Type
    {
        get => UnserializedData["_type"].ToObject<string>();
        set => UnserializedData["_time"] = value;
    }
}

