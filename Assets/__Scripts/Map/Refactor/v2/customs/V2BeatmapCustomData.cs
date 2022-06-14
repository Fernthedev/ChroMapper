using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2BeatmapCustomData : AbstractV2CustomData, IBeatmapCustomData
{
    public V2BeatmapCustomData([NotNull] IDictionary<string, JToken> dictionary) : base(dictionary)
    {
    }

    [JsonIgnore]
    public IEnumerable<ICustomEvent> CustomEvents
    {
        get => Get<JArray>("_customEvents")?.ToObject<List<V2CustomEvent>>();
        set => this["_customEvents"] = JArray.FromObject(value);
    }

    public override IBeatmapJSON Clone() => new V2BeatmapCustomData(this);


    public ICustomData ShallowClone() => throw new System.NotImplementedException();

    public ICustomData DeepCopy() => throw new System.NotImplementedException();
}
