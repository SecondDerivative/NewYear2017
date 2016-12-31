using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;

namespace SFMLApp
{
    class View
    {
        public RenderWindow MainForm { get; private set; }
        private int Width, Heigth;

        Sprite fon;

        public void InitEvents(EventHandler Close, EventHandler<KeyEventArgs> KeyDown, EventHandler<MouseButtonEventArgs> MouseDown, EventHandler<MouseButtonEventArgs> MouseUp, EventHandler<MouseMoveEventArgs> MouseMove)
        {
            MainForm.Closed += Close;
            MainForm.KeyPressed += KeyDown;
            MainForm.MouseButtonPressed += MouseDown;
            MainForm.MouseButtonReleased += MouseUp;
            MainForm.MouseMoved += MouseMove;
        }

        public View(int Width, int Heigth)
        {
            this.Width = Width;
            this.Heigth = Heigth;
            MainForm = new RenderWindow(new VideoMode((uint)Width, (uint)Heigth), "NewYear 2017", Styles.Titlebar | Styles.Close);
            fon = new Sprite(new Texture("data/fon.png"));
            fon.Position = new Vector2f(0, 0);
        }

        public void Clear()
        {
            MainForm.Clear(Color.White);
        }

        public void Clear(Color cl)
        {
            //MainForm.Clear(cl);
            MainForm.Draw(fon);
        }

        public void DrawText(string s, int x, int y, int size, Font Font, Color cl)
        {
            Text TextOut = new Text(s, Font);
            TextOut.CharacterSize = (uint)size;
            TextOut.Color = cl;
            TextOut.Position = new Vector2f(x, y);
            MainForm.Draw(TextOut);
        }

        private int CountChar(string s, char c)
        {
            int ans = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                    ++ans;
            }
            return ans;
        }

        public void DrawNote(Note note)
        {
            DrawText(note.Message, 180, 115, 20, Fonts.Arial, Color.Black);
            int cntline = 1 + CountChar(note.Message, '\n'); ;
            for (int i = 0; i < note.CanSay.Count; ++i)
            {
                DrawText((i + 1) + ": " + note.CanSay[i].Item1, 180, cntline * 30 + 115, 20, Fonts.Arial, Color.Black);
                cntline += 1 + CountChar(note.CanSay[i].Item1, '\n');
            }
            if (note.CanSay.Count == 0)
                DrawText("End of the game... Enter 1 for starn new game", 180, cntline * 30 + 115, 20, Fonts.Arial, Color.Black);
        }
    }
}
