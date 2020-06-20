using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEDDisplay
{
    public partial class Display : UserControl
    {

        public List<Pixel> pixels = new List<Pixel>();

        SolidBrush paintBrush = new SolidBrush(Color.Red);

        public int actualXClearPos;

        [Browsable(true), Category("Data")]
        public Color TextColor
        {

            get { return textColor; }
            set {
                textColor = value;
                Init();
            }
        }

        private Color textColor = Color.Red;

        [Browsable(true), Category("Data")]
        public Color BorderColor
        {

            get { return borderColor; }
            set
            {
                borderColor = value;
                Init();
            }
        }

        private Color borderColor = Color.Blue;

        [Browsable(true), Category("Data")]
        public string DisplayText
        {

            get { return text; }
            set
            {
                text = value;
                Init();
            }
        }

        private string text = "LCBLA";

        [Browsable(true), Category("Data")]
        public bool MoveRight
        {
            get { return moveRight; }
            set { moveRight = value;
                if (moveRight)
                {
                    speed = 1;
                }
                else
                {
                    speed = -1;
                }
            }
        }
        private bool moveRight = true;

        private int speed;

        [Browsable(true), Category("Data")]
        public int LettersSpacing
        {
            get { return lettersSpacing; }
            set { lettersSpacing = value;
                Init();
            }
        }
        private int lettersSpacing = 20;

        [Browsable(true), Category("Data")]
        public int LettersSize
        {
            get { return lettersSize; }
            set { lettersSize = value;
                Init();
            }
        }
        private int lettersSize = 10;


        [Browsable(true), Category("Data")]
        public int PanelWidth
        {
            get { return panelWidth; }
            set { panelWidth = value;

                panel1.CreateGraphics().Clear(Color.White);
                panel1.Width = value;
            }
        }
        private int panelWidth = 533;

        [Browsable(true), Category("Data")]
        public int PanelHeight
        {
            get { return panelHeight; }
            set { panelHeight = value;

                panel1.CreateGraphics().Clear(Color.White);
                panel1.Height = value;
            }
        }
        private int panelHeight = 100;

        [Browsable(true), Category("Data")]
        public int TickRate
        {
            get { return tickRate; }
            set { tickRate = value;
                timer1.Interval = tickRate;
            }
        }
        private int tickRate = 1000;

        public Letters letters = new Letters();

        public Display()
        {
            InitializeComponent();
            Init();
        }


        public void Init()
        {

            SetProperties();

            pixels.Clear();

            panel1.CreateGraphics().Clear(Color.White);

            GetLetters();
        }

        private void SetProperties()
        {
            letters.width = lettersSize;
            letters.height = lettersSize;

            if (LettersSize * 2 > LettersSpacing) LettersSpacing = LettersSize * 2;

            letters.xPos = lettersSpacing;
            letters.yPos = lettersSpacing;

            paintBrush.Color = textColor;
            letters.paintColor = textColor;
        }

        private void GetLetters()
        {
            int actualXOffset = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'A')
                {
                    pixels.AddRange(letters.GetA(actualXOffset));
                }

                if (text[i] == 'B')
                {
                    pixels.AddRange(letters.GetB(actualXOffset));
                }

                if (text[i] == 'C')
                {
                    pixels.AddRange(letters.GetC(actualXOffset));
                }

                if (text[i] == 'L')
                {
                    pixels.AddRange(letters.GetL(actualXOffset));
                }

                actualXOffset += 7 * letters.xPos;
            }
        }

        public void Draw(int xTransform)
        {
            Graphics graphics = panel1.CreateGraphics();

            ControlPaint.DrawBorder(graphics, panel1.ClientRectangle, borderColor, ButtonBorderStyle.Solid);

            foreach (var p in pixels)
            {
                p.xPos += xTransform * letters.xPos;
                paintBrush.Color = p.col;
                graphics.FillRectangle(paintBrush, p.xPos, p.yPos, p.width, p.height);
            }

            graphics.Dispose();


            if (speed > 0)
            {
                if (pixels[0].xPos > panel1.Width)
                {
                    Restart();
                }
            }
            else
            {
                if (pixels[pixels.Count - 1].xPos < 0)
                {
                    Restart();
                }
            }


        }

        private void Restart()
        {
            int lastXPos = pixels[pixels.Count - 1].xPos;
            int firstXPos = pixels[0].xPos;

            foreach (var p in pixels)
            {
                if (speed > 0)
                {
                    p.xPos -= lastXPos;
                }
                else
                {
                    p.xPos -= firstXPos - panel1.Width;
                }


            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw(speed);
        }

        public void OpenColorPicker()
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            // Sets the initial color select to the current text color.
            colorPicker.Color = borderColor;

            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
                borderColor = colorPicker.Color;
        }
    }

    public class Pixel
    {
        public int width;
        public int height;
        public int xPos;
        public int yPos;
        public Color col;

        public Pixel(int w, int h, int x, int y)
        {
            width = w;
            height = h;
            xPos = x;
            yPos = y;
        }
    }
}
