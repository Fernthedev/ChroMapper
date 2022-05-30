
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public interface IEventCustomData : ICustomData
{

    Color? Color { get; set; }

    [CanBeNull]
    IEnumerable<int> LightIDs { get; set; }

    [CanBeNull] ChromaGradient LightGadient { get; set; }
}

