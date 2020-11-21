using RDS.Interface;
using System;

namespace RDS.Core
{
    public class RDSCreatableObject : RDSObject, IRDSObjectCreator
    {
        public virtual IRDSObject CreateInstance()
        {
            return (IRDSObject)Activator.CreateInstance(GetType());
        }
    }
}
