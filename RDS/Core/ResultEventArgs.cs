using RDS.Interface;
using System.Collections.Generic;

namespace RDS.Core
{
    public class ResultEventArgs
    {
        public IEnumerable<IRDSObject> Result { get; set; }

        public ResultEventArgs(IEnumerable<IRDSObject> result)
        {
            Result = result;
        }
    }

    public delegate void ResultEventHandler(object sender, ResultEventArgs e);
}
