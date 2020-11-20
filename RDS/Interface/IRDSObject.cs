using RDS.Core;
using System;

namespace RDS.Interface
{
    public interface IRDSObject
    {
        int Weight { get; set; }
        bool Unique { get; set; }
        bool Always { get; set; }
        bool Enabled { get; set; }
        RDSTable RDSTable { get; set; }
        event EventHandler RDSPreResultEvaluation;
        event EventHandler RDSHit;
        event ResultEventHandler RDSPostResultEvaluation;
        void OnRDSPreResultEvaluation(EventArgs e);
        void OnRDSHit(EventArgs e);
        void OnRDSPostResultEvaluation(EventArgs e);
        string ToString(int indentationLevel);
    }
}
