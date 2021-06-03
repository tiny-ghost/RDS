using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Core
{
    public class RDSNullValue : RDSValue<object>
    {
        public RDSNullValue(int weight):base(null,weight,false,false,true)
        {

        }
    }
}
