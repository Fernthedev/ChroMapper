
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class AbstractV2CustomData : Dictionary<string, JToken>, ICustomData
{
    public bool isV3 => false;

    public IDictionary<string, JToken> UnserializedData { get; set; }
    public ICustomData ShallowClone() => throw new System.NotImplementedException();

    public ICustomData DeepCopy() => throw new System.NotImplementedException();
}

