using RDS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Core
{
    public class RDSObject : IRDSObject
    {
        public int Weight { get; set; }
        public bool Unique { get ; set ; }
        public bool Always { get ; set ; }
        public bool Enabled { get ; set ; }
        public RDSTable ContentTable { get ; set ; }

        public RDSObject():this(0)
        {

        }

        public RDSObject(int weight): this (weight, false,false,true)
        {

        }

        public RDSObject(int weight, bool unique, bool always, bool enabled)
        {
            Weight = weight;
            Unique = unique;
            Always = always;
            Enabled = enabled;
            ContentTable = null;
            
        }

        public event EventHandler RDSHit;
        public event EventHandler RDSPreResultEvaluation;
        public event ResultEventHandler RDSPostResultEvaluation;

        public virtual void OnRDSHit(EventArgs e)
        {
            RDSHit?.Invoke(this, e);
        }

        public virtual void OnRDSPostResultEvaluation(ResultEventArgs e)
        {
            RDSPostResultEvaluation?.Invoke(this, e);
        }

        public virtual void OnRDSPreResultEvaluation(EventArgs e)
        {
            RDSPreResultEvaluation?.Invoke(this, e);
        }

        public string ToString(int indentationLevel)
        {
            string indent = "".PadRight(2 * indentationLevel, ' ');
            return String.Format($"{indent}RDSObject: {this.GetType().Name}. W:{Weight} U:{Unique} A:{Always} E{Enabled}");
        }
        public override string ToString()
        {
            return ToString(0);
        }
    }
}
