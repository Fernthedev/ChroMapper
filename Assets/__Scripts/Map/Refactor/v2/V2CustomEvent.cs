
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2CustomEvent : ICustomEvent
{
    [JsonIgnore] public bool isV3 => false;
    public IBeatmapJSON Clone() => throw new System.NotImplementedException();

    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; set; }
    
        
    [JsonIgnore]
    public ICustomData CustomData { get; set; }
    
    [JsonProperty("_time")]
    public float Time { get; set; }
    
    [JsonProperty("_type")]
    public string Type { get; set; }
    
    public 
}

