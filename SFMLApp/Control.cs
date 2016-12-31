﻿using System;
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
            Note[] allNote = new Note[3];
            allNote[0] = new Note("Это обучение. Чтобы выбрать первый вариант, нажмине 1");
            allNote[1] = new Note("Вы проснулись в холодной пещере. Ваша голова болит, вы ничего не помните");
            allNote[2] = new Note("End");
            allNote[0].CanSay.Add(new Tuple<string, Note>("Продолжить", allNote[1]));
            allNote[0].CanSay.Add(new Tuple<string, Note>("Заново", allNote[0]));
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
