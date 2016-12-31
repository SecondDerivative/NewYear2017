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
            MainForm = new RenderWindow(new VideoMode((uint)Width, (uint)Heigth), "SFML.net", Styles.Titlebar | Styles.Close);
        }

        public void Clear()
        {
            MainForm.Clear(Color.White);
        }

        public void Clear(Color cl)
        {
            MainForm.Clear(cl);
        }

        public void DrawText(string s, int x, int y, int size, Font Font, Color cl)
        {
            Text TextOut = new Text(s, Font);
            TextOut.CharacterSize = (uint)size;
            TextOut.Color = cl;
            TextOut.Position = new Vector2f(x, y);
            MainForm.Draw(TextOut);
        }

        public void DrawNote(Note note)
        {
            DrawText(note.Message, 20, 20, 20, Fonts.Arial, Color.Black);
            for (int i = 0; i < note.CanSay.Count; ++i)
            {
                DrawText((i + 1) + ": " + note.CanSay[i].Item1, 20, (i + 1) * 30 + 20, 20, Fonts.Arial, Color.Black);
            }
            if (note.CanSay.Count == 0)
                DrawText("End of the game... Enter 1 for starn new game", 20, 30 + 20, 20, Fonts.Arial, Color.Black);
        }
    }
}
