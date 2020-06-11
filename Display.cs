using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEDDisplay
{
    public partial class Display : UserControl
    {

        public List<Pixel> pixels = new List<Pixel>();

        SolidBrush paintBrush = new SolidBrush(Color.Red);

        public Color borderColor = Color.DarkBlue;

        public int actualXClearPos;

        public string text = "LCBLA";

        public int speed;

        public int tickRate;

        public Letters letters = new Letters();

        public Display()
        {
            InitializeComponent();
        }


        public void Init()
        {
            if (!IsComponentInitable) return;

            SetProperties();

            pixels.Clear();

            panel1.CreateGraphics().Clear(Color.White);

            GetLetters();

            timer1.Enabled = true;
            timer1.Interval = tickRate;
        }

        private void SetProperties()
        {
            letters.width = Int32.Parse(textBox2.Text);
            letters.height = Int32.Parse(textBox2.Text);

            letters.xPos = Int32.Parse(textBox4.Text);
            letters.yPos = Int32.Parse(textBox4.Text);

            panel1.Width = Int32.Parse(textBox6.Text);
            panel1.Height = Int32.Parse(textBox5.Text);

            text = textBox1.Text.ToUpper();

            if (checkBox1.Checked)
            {
                speed = -1;
            }
            else
            {
                speed = 1;
            }

            tickRate = Int32.Parse(textBox3.Text);
        }

        private bool IsComponentInitable()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Wypełnij wszystkie komórki", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Int32.Parse(textBox2.Text) < 0)
            {
                MessageBox.Show("Wielkość musi być nieujemna", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Int32.Parse(textBox4.Text) < 2 * Int32.Parse(textBox2.Text))
            {
                MessageBox.Show("Odstępy musząbyć conajmniej 2 razy większe od wielkości", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Int32.Parse(textBox5.Text) <= 0)
            {
                MessageBox.Show("Szerokość wyświetlacza musi być dodatnia", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Int32.Parse(textBox6.Text) <= 0)
            {
                MessageBox.Show("Wysokość wyświetlacza musi być dodatnia", "Error Title", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
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

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Draw(speed);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Init();
        }

        private void checkBox2_Click_1(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }

        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            letters.OpenColorPicker();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenColorPicker();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
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
