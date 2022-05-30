using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;

public class BeatmapCustomEvent : BeatmapObject<IEventCustomData>
{
    [JsonProperty("_type")]
    public string Type;
    
    [JsonProperty("_time")]
    public override float Time { get; set; }

    [JsonProperty("_data")]
    public sealed override ICustomData CustomData { get; set; }
    
    [JsonIgnore]
    public override ObjectType BeatmapType { get; set; } = ObjectType.CustomEvent;

    public BeatmapCustomEvent(float time, string type, IEventCustomData data)
    {
        Time = time;
        Type = type;
        CustomData = data;
    }
    
    protected override bool IsConflictingWithObjectAtSameTime(BeatmapObject other, bool deletion)
    {
        if (deletion)
            return Type == (other as BeatmapCustomEvent)?.Type;
        return false;
    }

    public override void Apply(BeatmapObject originalData)
    {
        base.Apply(originalData);

        if (originalData is BeatmapCustomEvent ev) Type = ev.Type;
    }

    public override JSONNode GetOrCreateCustomData() => throw new NotImplementedException();
}
