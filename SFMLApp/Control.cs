using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using System.IO;

namespace SFMLApp
{
    class Control
    {
        public View view { get; private set; }
        private int Width, Heigth;

        private Dialogue dialogue;
        private Note StartNote;

        public Control(int Width, int Heigth)
        {
            this.Width = Width;
            this.Heigth = Heigth;
            view = new View(Width, Heigth);
            view.InitEvents(Close, KeyDown, MouseDown, MouseUp, MouseMove);
            StreamReader fl = File.OpenText("data/data.txt");
            int cnt = int.Parse(fl.ReadLine());
            Note[] allNote = new Note[cnt];
            List<Tuple<int, string> >[] mem = new List<Tuple<int, string> >[cnt];
            for (int i = 0; i < cnt; ++i)
            {
                string s = fl.ReadLine();
                allNote[i] = new Note(s.Replace('#', '\n'));
                mem[i] = new List<Tuple<int, string>>();
                int cntedge = int.Parse(fl.ReadLine());
                for (int j = 0; j < cntedge; ++j)
                {
                    string a = fl.ReadLine().Replace('#', '\n');
                    int yk = int.Parse(fl.ReadLine());
                    mem[i].Add(new Tuple<int, string>(yk, a));
                }
            }
            for (int i = 0; i < cnt; ++i)
                for (int j = 0; j < mem[i].Count; j++)
                    allNote[i].CanSay.Add(new Tuple<string, Note>(mem[i][j].Item2, allNote[mem[i][j].Item1]));
            fl.Close();
            StartNote = allNote[0];
            dialogue = new Dialogue(StartNote);
        }

        public void UpDate(long time)
        {
            view.Clear(Color.White);
            view.DrawNote(dialogue.now);
            if (time > 0)
                view.DrawText((1000 / time).ToString(), 5, 5, 10, Fonts.Arial, Color.White);
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            int ch = (int)e.Code - (int)Keyboard.Key.Num0;
            if (ch > 0 && ch <= dialogue.CountAns())
            {
                dialogue.Next(ch - 1);
                return;
            }
            if (dialogue.IsEnd() && ch == 1)
            {
                dialogue = new Dialogue(StartNote);
            }
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void MouseMove(object sender, MouseMoveEventArgs e)
        {
        }

        public void Close(object send, EventArgs e)
        {
            ((RenderWindow)send).Close();
        }
    }
}
