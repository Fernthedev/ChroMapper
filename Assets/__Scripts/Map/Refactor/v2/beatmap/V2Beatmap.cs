
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
    public IBeatmapJSON Clone() => new V2Beatmap(new Dictionary<string, JToken>(UnserializedData), UntypedCustomData?.Clone() as V2BeatmapCustomData, new List<INote>(Notes), new List<IEvent>(Events), new List<IObstacle>(Obstacles));

    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; }

    [JsonProperty("_customData")] 
    public IBeatmapCustomData CustomData { get; }
    public ICustomData UntypedCustomData => CustomData;

    [JsonProperty("_notes")]
    [JsonConverter(typeof(V2NoteList))]
    public IList<INote> Notes { get; }

    [JsonProperty("_events")]
    [JsonConverter(typeof(V2EventList))]
    public IList<IEvent> Events { get; }

    [JsonConverter(typeof(V2ObstacleList))]
    [JsonProperty("_obstacles")]
    public IList<IObstacle> Obstacles { get; }

    [JsonConverter(typeof(V2WaypointList))]
    [JsonProperty("_waypoints")]
    public IList<IWaypoint> Waypoints { get; }
    
    [JsonConverter(typeof(V2SliderList))]
    [JsonProperty("_sliders")]
    public IList<ISlider> Sliders { get; }
    
    public IBeatmapCustomData BeatmapCustomData { get => UnserializedData["_customData"].ToObject<V2BeatmapCustomData>();
        set => UnserializedData["_customData"] = JToken.FromObject(value);
    }
}

