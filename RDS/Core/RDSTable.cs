using RDS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDS.Core
{
    public class RDSTable : IRDSTable
    {
        public int Count { get; set; }
        public IEnumerable<IRDSObject> Content { get; set; }
        public int Weight { get; set; }
        public bool Unique { get; set; }
        public bool Always { get; set; }
        public bool Enabled { get; set; }
        public RDSTable ContentTable { get; set; }

        private List<IRDSObject> _contents = null;
        private List<IRDSObject> _unique = new List<IRDSObject>();

        public RDSTable()
            :this(null,1,100,false,false,true)
        {
        }

        public RDSTable(IEnumerable<IRDSObject> content, int count, int weight)
            : this(content, count, weight, false, false, true)
        {
        }

        public RDSTable(IEnumerable<IRDSObject> content, int count, int weight,bool unique,bool always,bool enabled)
        {
            if (content != null)
            {
                _contents = content.ToList();
            }
            else
            {
                ClearContents();
            }
            Count = count;
            Weight = weight;
            Unique = unique;
            Always = always;
            Enabled = enabled;
        }

        public event EventHandler RDSHit;
        public event EventHandler RDSPreResultEvaluation;
        public event ResultEventHandler RDSPostResultEvaluation;

        public virtual void ClearContents()
        {
            _contents = new List<IRDSObject>();
        }
        #region Add Entry
        public virtual void AddEntry(IRDSObject entry)
        {
            _contents.Add(entry);
            entry.ContentTable = this;
        }

        public virtual void AddEntry(IRDSObject entry,int weight)
        {
            _contents.Add(entry);
            entry.Weight = weight;
            entry.ContentTable = this;
        }

        public virtual void AddEntry(IRDSObject entry, int weight,bool unique, bool always, bool enabled)
        {
            _contents.Add(entry);
            entry.Weight = weight;
            entry.Unique = unique;
            entry.Always = always;
            entry.Enabled = enabled;
            entry.ContentTable = this;
        }
        #endregion

        public virtual void OnRDSHit(EventArgs e)
        {
            RDSHit?.Invoke(this, e);
        }

        public void OnRDSPostResultEvaluation(ResultEventArgs e)
        {
            RDSPostResultEvaluation?.Invoke(this, e);
        }

        public void OnRDSPreResultEvaluation(EventArgs e)
        {
            RDSPreResultEvaluation?.Invoke(this, e);
        }

        private void AddToResult(List<IRDSObject> returnValue, IRDSObject obj)
        {
            if(!obj.Unique || !_unique.Contains(obj))
            {
                if(obj.Unique)
                {
                    _unique.Add(obj);
                }
                if(!(obj is RDSNullValue))
                {
                    if(obj is RDSTable)
                    {
                        returnValue.AddRange(((IRDSTable)obj).Result());
                    }
                    else
                    {
                        IRDSObject itemToAdd = obj;
                        if (obj is IRDSObjectCreator creator)
                        {
                            itemToAdd = creator.CreateInstance();
                        }
                        returnValue.Add(itemToAdd);
                        obj.OnRDSHit(EventArgs.Empty);                       
                    }
                }
                else
                {
                    obj.OnRDSHit(EventArgs.Empty);
                }
            }
        }
        public virtual IEnumerable<IRDSObject> Result()
        {
            var returnValue = new List<IRDSObject>();
            _unique = new List<IRDSObject>();

            foreach (var obj in _contents)
            {
                obj.OnRDSPreResultEvaluation(EventArgs.Empty);
            }

            foreach (var obj in _contents.Where(obj => obj.Always && obj.Enabled))
            {
                AddToResult(returnValue, obj);
            }
            var alwaysCount = _contents.Count(obj => obj.Always && obj.Enabled);
            var realDropCount = Count - alwaysCount;

            if(realDropCount > 0)
            {
                for (var dropCount = 0;dropCount < realDropCount;dropCount++)
                {
                    var dropables = _contents.Where(obj => obj.Enabled && !obj.Always);
                    var hitWeight = RDSRandom.GetIntValue(dropables.Sum(obj => obj.Weight));
                    var runningWeight = 0;
                    foreach (var obj in dropables)
                    {
                        runningWeight += obj.Weight;
                        if(hitWeight < runningWeight)
                        {
                            AddToResult(returnValue, obj);
                            break;
                        } 
                    }
                }
            }

            var resultEvent = new ResultEventArgs(returnValue);

            foreach (var obj in returnValue)
            {
                obj.OnRDSPostResultEvaluation(resultEvent);
            }

            return returnValue;
        }

        public string ToString(int indentationLevel)
        {
            var indent = "".PadRight(2 * indentationLevel, ' ');
            var sb = new StringBuilder();
            sb.AppendFormat($"{indent} RDSTable: {this.GetType().Name}. W: {Weight} U: {Unique} A: {Always} E: {Enabled}");
            foreach (IRDSObject obj in _contents)
            {
                sb.AppendLine($"{indent}{obj.ToString(indentationLevel+1)}");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToString(0);
        }
    }
}
