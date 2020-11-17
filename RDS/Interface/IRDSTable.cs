using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Interface
{
    public interface IRDSTable : IRDSObject
    {
        int Count { get; set; }
        IEnumerable<IRDSObject> Content { get; set; }
        IEnumerable<IRDSObject> Result { get; set; }
    }
}
