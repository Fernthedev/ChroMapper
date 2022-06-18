
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

public abstract class V2CustomBeatmapItem<T> : V2BeatmapItem, ICustomBeatmapItem, ICustomBeatmapItem<T> where T: class, ICustomData
{
    public V2CustomBeatmapItem(IDictionary<string, JToken> unserializedData) : base(unserializedData) => CustomData = unserializedData["_customData"].ToObject<T>();

    public bool isV3 => false;

    public T CustomData { get; }
    public ICustomData UntypedCustomData => ((ICustomBeatmapItem<T>)this).CustomData;
}
