using RDS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Core
{
    class RDSValue<T> : IRDSValue<T>
    {
        public T Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Weight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Unique { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Always { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public RDSTable ContentTable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler RDSPreResultEvaluation;
        public event EventHandler RDSHit;
        public event ResultEventHandler RDSPostResultEvaluation;

        public void OnRDSHit(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnRDSPostResultEvaluation(ResultEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnRDSPreResultEvaluation(EventArgs e)
        {
            throw new NotImplementedException();
        }

        public string ToString(int indentationLevel)
        {
            throw new NotImplementedException();
        }
    }
}
