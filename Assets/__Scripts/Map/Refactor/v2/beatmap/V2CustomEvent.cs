
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class V2CustomEvent : V2CustomBeatmapItem<V2CustomEventCustomData>, ICustomEvent
{
    public V2CustomEvent(IDictionary<string, JToken> unserializedData) : base(unserializedData) => UnserializedData = unserializedData;
    
    public override IBeatmapJSON Clone() => new V2CustomEvent(new Dictionary<string, JToken>(UnserializedData));

    public IDictionary<string, JToken> UnserializedData { get; }
    
    public string Type
    {
        get => UnserializedData["_type"].ToObject<string>();
        set => UnserializedData["_type"] = value;
    }

    public ICustomEventCustomData CustomData
    {
        get => TypedCustomData;
        set => SetCustomData(value);
    }
}

