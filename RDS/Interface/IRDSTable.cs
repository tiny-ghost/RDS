﻿using System.Collections.Generic;

namespace RDS.Interface
{
    public interface IRDSTable : IRDSObject
    {
        int Count { get; set; }
        IEnumerable<IRDSObject> Content { get; set; }
        IEnumerable<IRDSObject> Result();
    }
}
