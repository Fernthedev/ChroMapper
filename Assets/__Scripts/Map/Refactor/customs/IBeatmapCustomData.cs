using System.Collections.Generic;

public interface IBeatmapCustomData : ICustomData
{

    IEnumerable<ICustomEvent> CustomEvents { get; set; }
    
    

}

