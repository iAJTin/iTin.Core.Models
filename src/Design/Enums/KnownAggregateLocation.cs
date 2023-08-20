
using System;
using System.CodeDom.Compiler;

namespace iTin.Core.Models.Design.Enums;

/// <summary>
/// Specifies known locations for aggregate functions
/// </summary>
[Serializable]
[GeneratedCode("System.Xml", "4.0.30319.17929")]
public enum KnownAggregateLocation
{
    /// <summary>
    /// This aggregate is vertically aligned at the top.
    /// </summary>
    Top,

    /// <summary>
    /// This aggregate is vertically aligned at the bottom.
    /// </summary>
    Bottom
}
