
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class V2Beatmap : IBeatmap
{
    [JsonConstructor]
    public V2Beatmap(IDictionary<string, JToken> unserializedData, [CanBeNull] V2BeatmapCustomData customData, IList<INote> notes, IList<IEvent> events, IList<IObstacle> obstacles)
    {
        UnserializedData = unserializedData;
        CustomData = customData;
        Notes = notes;
        Events = events;
        Obstacles = obstacles;
    }

    public bool isV3 => false;
    public IBeatmapJSON Clone() => new V2Beatmap(new Dictionary<string, JToken>(UnserializedData), CustomData?.Clone() as V2BeatmapCustomData, new List<INote>(Notes), new List<IEvent>(Events), new List<IObstacle>(Obstacles));
    
    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; set; }

    [JsonProperty("_customData")]
    public ICustomData CustomData { get; set; }
    
    [JsonProperty("_notes")]
    public IList<INote> Notes { get; set; }
    
    [JsonProperty("_events")]
    public IList<IEvent> Events { get; set; }
    
    [JsonProperty("_obstacles")]
    public IList<IObstacle> Obstacles { get; set; }



}

