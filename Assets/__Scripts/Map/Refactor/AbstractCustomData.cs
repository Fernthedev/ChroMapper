
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public abstract class AbstractCustomData : Dictionary<string, JToken>, ICustomData
{
    [JsonExtensionData]
    public IDictionary<string, JToken> UnserializedData { get; set; }

    public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator() => throw new System.NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator() {
        
    }

    public bool ContainsKey(string key) => throw new System.NotImplementedException();

    public bool TryGetValue(string key, out JToken value) => throw new System.NotImplementedException();

    public ICustomData ShallowClone() => throw new System.NotImplementedException();

    public ICustomData DeepCopy() => throw new System.NotImplementedException();
}

