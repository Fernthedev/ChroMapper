using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// An item that is stored in a beatmap JSON
/// </summary>
public interface IBeatmapJSON
{
    [JsonIgnore] bool isV3 { get; }

    IBeatmapJSON Clone();

    [JsonExtensionData] IDictionary<string, JToken> UnserializedData { get; }
}


public interface IBeatmapCustomJSON : IBeatmapJSON
{
    [CanBeNull]
    [JsonIgnore]
    ICustomData UntypedCustomData { get; }
}

public interface IBeatmapItem : IBeatmapJSON
{
    float Time { get; set; }
}

public interface ICustomBeatmapItem : IBeatmapItem, IBeatmapCustomJSON
{
}

public interface ICustomBeatmapItem<T> : IBeatmapItem, IBeatmapCustomJSON where T: class, ICustomData
{
    [CanBeNull] 
    public T CustomData { get; }
}

public interface IBeatmapObject<T> : ICustomBeatmapItem<T> where T : class, IObjectCustomData
{
    public int LineIndex { get; set; }
}
