
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

public abstract class V2BeatmapItem : IBeatmapItem
{
    public bool isV3 => false;
    public abstract IBeatmapJSON Clone();


    public V2BeatmapItem(IDictionary<string, JToken> unserializedData) => UnserializedData = unserializedData;
    
    public IDictionary<string, JToken> UnserializedData { get; }
    
    public float Time
    {
        get => UnserializedData["_time"].ToObject<float>();
        set => UnserializedData["_time"] = value;
    }
}

public abstract class V2CustomBeatmapItem<T> : V2BeatmapItem, ICustomBeatmapItem where T: class, ICustomData
{
    public V2CustomBeatmapItem(IDictionary<string, JToken> unserializedData) : base(unserializedData) {}

    public bool isV3 => false;

    // For whatever reason 
    [CanBeNull]
    public T TypedCustomData 
    {
        get => UnserializedData["_customData"]?.ToObject<T>();
        set => SetCustomData(value);
    }

    public ICustomData UntypedCustomData => TypedCustomData;

    protected void SetCustomData([CanBeNull] ICustomData customData)
    {
        if (customData != null)
        {
            UnserializedData["_customData"] = JObject.FromObject(customData.UnserializedData);
        }
        else
        {
            UnserializedData.Remove("_customData");
        }
    }
}
