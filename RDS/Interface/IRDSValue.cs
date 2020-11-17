using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Interface
{
    interface IRDSValue<T> : IRDSObject
    {
        T Value { get; set; }
    }
}
