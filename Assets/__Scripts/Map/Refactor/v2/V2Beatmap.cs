
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2Beatmap : IBeatmap
{
    public bool isV3 => false;
    public IBeatmapJSON Clone() => new V2Beatmap(Notes, Events, Obstacles, CustomEvents);
    
    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; set; }

    public ICustomData CustomData { get; set; }
    public IList<INote> Notes { get; set; }
    public IList<IEvent> Events { get; set; }
    public IList<IObstacle> Obstacles { get; set; }
    public IList<ICustomEvent> CustomEvents { get; set; }
    
    
}

