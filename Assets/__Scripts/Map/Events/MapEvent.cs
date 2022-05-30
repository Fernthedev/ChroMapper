using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public sealed class MapEvent : BeatmapObject<IEventCustomData>
{
    /*
     * Event Type constants
     */

    public const int EventTypeBackLasers = 0;
    public const int EventTypeRingLights = 1;
    public const int EventTypeLeftLasers = 2;
    public const int EventTypeRightLasers = 3;
    public const int EventTypeRoadLights = 4;
    public const int EventTypeBoostLights = 5;
    public const int EventTypeCustomLight2 = 6;
    public const int EventTypeCustomLight3 = 7;
    public const int EventTypeRingsRotate = 8;
    public const int EventTypeRingsZoom = 9;
    public const int EventTypeCustomLight4 = 10;
    public const int EventTypeCustomLight5 = 11;
    public const int EventTypeLeftLasersSpeed = 12;
    public const int EventTypeRightLasersSpeed = 13;
    public const int EventTypeEarlyRotation = 14;
    public const int EventTypeLateRotation = 15;
    public const int EventTypeCustomEvent1 = 16;
    public const int EventTypeCustomEvent2 = 17;

    /*
     * Light value constants
     */

    public const int LightValueOff = 0;

    public const int LightValueBlueON = 1;
    public const int LightValueBlueFlash = 2;
    public const int LightValueBlueFade = 3;

    public const int LightValueRedON = 5;
    public const int LightValueRedFlash = 6;
    public const int LightValueRedFade = 7;

    public static readonly int[] LightValueToRotationDegrees = { -60, -45, -30, -15, 15, 30, 45, 60 };
    
    [JsonIgnore]
    public int PropId = -1;
    
    
    [JsonProperty("_type")]
    [FormerlySerializedAs("_type")] public int Type;
    
    [JsonProperty("_value")]
    [FormerlySerializedAs("_value")] public int Value;

    // TODO: Remove
    [JsonProperty("_lightGradient")]
    [CanBeNull]
    [DefaultValue(null)]
    [FormerlySerializedAs("_lightGradient")] 
    public ChromaGradient LightGradient;

    /*
     * MapEvent logic
     */
    public MapEvent(float time, int type, int value, IEventCustomData customData = null)
    {
        Time = time;
        Type = type;
        Value = value;
        CustomData = customData;
        LightGradient = customData.LightGadient;
    }

    [JsonIgnore]
    public bool IsRotationEvent => Type == EventTypeEarlyRotation || Type == EventTypeLateRotation;
    
    [JsonIgnore]
    public bool IsRingEvent => Type == EventTypeRingsRotate || Type == EventTypeRingsZoom;
    
    [JsonIgnore]
    public bool IsLaserSpeedEvent => Type == EventTypeLeftLasersSpeed || Type == EventTypeRightLasersSpeed;
    
    [JsonIgnore]
    public bool IsUtilityEvent => IsRotationEvent || IsRingEvent || IsLaserSpeedEvent ||
                                  Type == EventTypeBoostLights || IsInterscopeEvent;

    [JsonIgnore]
    public bool IsInterscopeEvent => Type == EventTypeCustomEvent1 || Type == EventTypeCustomEvent2;
    
    [JsonIgnore]
    public bool IsLegacyChromaEvent => Value >= ColourManager.RgbintOffset;
    
    [JsonIgnore]
    public bool IsChromaEvent => CustomData?.ContainsKey("_color") ?? false;
    
    [JsonIgnore]
    public bool IsPropogationEvent => PropId > -1; //_customData["_lightID"].IsArray
    
    [JsonIgnore]
    public bool IsLightIdEvent => CustomData?.ContainsKey("_lightID") ?? false;

    [JsonIgnore]
    public override ObjectType BeatmapType { get; set; } = ObjectType.Event;

    public override float Time { get; set; }

    public static bool IsBlueEventFromValue(int value) => value == LightValueBlueON ||
                                                          value == LightValueBlueFlash ||
                                                          value == LightValueBlueFade;

    public int? GetRotationDegreeFromValue()
    {
        //Mapping Extensions precision rotation from 1000 to 1720: 1000 = -360 degrees, 1360 = 0 degrees, 1720 = 360 degrees
        var val = CustomData != null && CustomData.HasKey("_queuedRotation")
            ? CustomData["_queuedRotation"].AsInt
            : Value;
        if (val >= 0 && val < LightValueToRotationDegrees.Length) return LightValueToRotationDegrees[val];
        if (val >= 1000 && val <= 1720) return val - 1360;
        return null;
    }

    public Vector2? GetPosition(CreateEventTypeLabels labels, EventsContainer.PropMode mode, int prop)
    {
        if (IsLightIdEvent) PropId = labels.LightIdsToPropId(Type, LightId) ?? -1;

        if (mode == EventsContainer.PropMode.Off)
        {
            return new Vector2(
                labels.EventTypeToLaneId(Type) + 0.5f,
                0.5f
            );
        }

        if (Type != prop) return null;

        if (IsLightIdEvent)
        {
            var x = mode == EventsContainer.PropMode.Prop ? PropId : -1;

            if (x < 0)
                x = LightId.Length > 0 ? labels.LightIDToEditor(Type, LightId[0]) : -1;

            return new Vector2(
                x + 1.5f,
                0.5f
            );
        }

        return new Vector2(
            0.5f,
            0.5f
        );
    }


    protected override bool IsConflictingWithObjectAtSameTime(BeatmapObject other, bool deletion)
    {
        if (other is MapEvent @event)
        {
            var lightId = IsLightIdEvent ? LightId : null;
            var otherLightId = @event.IsLightIdEvent ? @event.LightId : null;
            var lightIdEquals = lightId?.Length == otherLightId?.Length &&
                                (lightId == null || lightId.All(x => otherLightId.Contains(x)));

            return Type == @event.Type && lightIdEquals;
        }

        return false;
    }

    public override void Apply(BeatmapObject originalData)
    {
        base.Apply(originalData);

        if (originalData is MapEvent obs)
        {
            Type = obs.Type;
            Value = obs.Value;
            LightGradient = obs.LightGradient?.Clone();
        }
    }

    public override JSONNode GetOrCreateCustomData() => throw new NotImplementedException();
}
