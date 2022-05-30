using System.Collections.Generic;

// The amount of interfaces used here feels so wrong
// and yet it feels so right


public interface IBeatmap : IBeatmapCustomJSON
{
    IList<INote> Notes { get; set; }
    IList<IEvent> Events { get; set; }
    IList<IObstacle> Obstacles { get; set; }
    IList<ICustomEvent> CustomEvents { get; set; }


    // TODO: The rest
}

public interface INote : IBeatmapObject
{
}

public interface IObstacle : IBeatmapObject
{
}

public interface IEvent : ICustomBeatmapItem
{
}

public interface ICustomEvent : ICustomBeatmapItem
{
    public string Type { get; set; }
}
