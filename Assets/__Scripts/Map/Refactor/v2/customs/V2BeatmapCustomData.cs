
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2BeatmapCustomData : Dictionary<string, JToken>, IBeatmapCustomData
{
    public bool isV3 => false;

    [JsonIgnore]
    public IEnumerable<ICustomEvent> CustomEvents { get; set; }
    
    [JsonProperty("_customEvents")]
    public IList<V2CustomEvent> CustomEventsV2 { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; set; }

    public V2BeatmapCustomData()
    {
    }

    public V2BeatmapCustomData([NotNull] IDictionary<string, JToken> dictionary) : base(dictionary)
    {
    }

    public V2BeatmapCustomData(int capacity) : base(capacity)
    {
    }
    
    public IBeatmapJSON Clone() => new V2BeatmapCustomData(this);


    public ICustomData ShallowClone() => throw new System.NotImplementedException();

    public ICustomData DeepCopy() => throw new System.NotImplementedException();

}

