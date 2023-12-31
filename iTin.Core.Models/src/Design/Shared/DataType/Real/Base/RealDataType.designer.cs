﻿
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace iTin.Core.Models.Design;

[Serializable]
//[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlInclude(typeof(NumericDataType))]
[GeneratedCode("System.Xml", "4.0.30319.18033")]
[XmlType(Namespace = "http://schemas.itin.com/models/core/v1.0")]
public abstract partial class RealDataType : BaseDataType
{
}
