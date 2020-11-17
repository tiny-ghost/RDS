using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Interface
{
    public interface IRDSObjectCreator
    {
        IRDSObject CreateInstance();
    }
}
