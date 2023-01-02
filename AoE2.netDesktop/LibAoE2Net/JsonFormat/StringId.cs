﻿#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Runtime.Serialization;

/// <summary>
/// String ID.
/// </summary>
[DataContract]
public class StringId
{
    [DataMember(Name = "id")]
    public int? Id { get; set; }

    [DataMember(Name = "string")]
    public string String { get; set; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{Id}:{String}";
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
