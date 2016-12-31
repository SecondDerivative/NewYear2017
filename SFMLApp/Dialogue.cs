using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLApp
{
    class Dialogue
    {
        public Note now { get; private set; }
        public Dialogue(Note nt)
        {
            now = nt;
        }
        public void Next(int t)
        {
            now = now.CanSay[t].Item2;
        }
        public bool IsEnd()
        {
            return now.CanSay.Count == 0;
        }
        public int CountAns()
        {
            return now.CanSay.Count;
        }
    }

    class Note
    {
        public string Message { get; private set; }
        public List<Tuple<string, Note>> CanSay { get; private set; }
        public Note(string msg)
        {
            Message = msg;
            CanSay = new List<Tuple<string, Note>>();
        }
    }
}
