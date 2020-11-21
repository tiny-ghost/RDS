using RDS.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDS.Core
{
    public class RDSValue<T> : IRDSValue<T>
    {
        public T Value { get { return _value; } set { _value = value; } }
        public int Weight { get; set; }
        public bool Unique { get; set; }
        public bool Always { get; set; }
        public bool Enabled { get; set; }
        public RDSTable ContentTable { get; set; }

        private T _value;

        public RDSValue(T value, int weight) : this(value, weight, false, false, true)
        {
        }

        public RDSValue(T value, int weight, bool unique,bool always, bool enabled)
        {
            _value = value;
            Weight = weight;
            Unique = unique;
            Always = always;
            Enabled = enabled;
            ContentTable = null;
        }



        public event EventHandler RDSPreResultEvaluation;
        public event EventHandler RDSHit;
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
            var sb = new StringBuilder();
            sb.AppendFormat($"{indent} RDSValue: {this.GetType().Name}. W: {Weight} U: {Unique} A: {Always} E: {Enabled}");
            return sb.ToString();
        }
    }
}
