
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class V2Slider : V2BeatmapObject<V2SliderCustomData>, ISlider
{
    public V2Note(IDictionary<string, JToken> unserializedData) : base(unserializedData)
    {
    }

    public override IBeatmapJSON Clone() => new V2Note(new Dictionary<string, JToken>(UnserializedData));

    public int Type
    {
        get => UnserializedData["_type"].ToObject<int>();
        set => UnserializedData["_type"] = value;
    }

    public int CutDirection
    {
        get => UnserializedData["_cutDirection"].ToObject<int>();
        set => UnserializedData["_cutDirection"] = value;
    }

    public int LineLayer
    {
        get => UnserializedData["_lineLayer"].ToObject<int>();
        set => UnserializedData["_lineLayer"] = value;
    }
}

