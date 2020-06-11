using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LEDDisplay
{
    public class Letters
    {
        public int width = 5;
        public int height = 5;
        public int xPos = 10;
        public int yPos = 10;

        public Color paintColor = Color.Red;

        public List<Pixel> GetA(int xOffset)
        {
            List<Pixel> pixels = new List<Pixel>();

            pixels.AddRange(AddSpace(xOffset - xPos));

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var p = new Pixel(width, height, xOffset + i * xPos, 10 + j * yPos);

                    if ((i == 2 && j == 0) || ((i == 1 || i == 3) && j == 1) || ((i == 1 || i == 2 || i == 3) && j == 2) || ((i == 0 || i == 4) && (j == 3 || j == 4)))
                    {
                        p.col = paintColor;
                    }
                    else
                    {
                        p.col = Color.White;
                    }

                    pixels.Add(p);
                }

            }

            pixels.AddRange(AddSpace(xOffset + (5 * xPos)));

            return pixels;
        }

        public List<Pixel> GetB(int xOffset)
        {
            List<Pixel> pixels = new List<Pixel>();

            pixels.AddRange(AddSpace(xOffset - xPos));

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var p = new Pixel(width, height, xOffset + i * xPos, 10 + j * yPos);

                    if (i == 0 || ((i == 1 || i == 2 || i == 3) && (j == 0 || j == 2 || j == 4)) || ((i == 4 && (j == 1 || j == 3))))
                    {
                        p.col = paintColor;
                    }
                    else
                    {
                        p.col = Color.White;
                    }

                    pixels.Add(p);
                }

            }

            pixels.AddRange(AddSpace(xOffset + (5 * xPos)));

            return pixels;
        }

        public List<Pixel> GetC(int xOffset)
        {
            List<Pixel> pixels = new List<Pixel>();

            pixels.AddRange(AddSpace(xOffset - xPos));

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var p = new Pixel(width, height, xOffset + i * xPos, 10 + j * yPos);

                    if ((i == 0 && (j == 1 || j == 2 || j == 3) || ((i == 1 || i == 2 || i == 3 || i == 4) && (j == 0 || j == 4))))
                    {
                        p.col = paintColor;
                    }
                    else
                    {
                        p.col = Color.White;
                    }

                    pixels.Add(p);
                }

            }

            pixels.AddRange(AddSpace(xOffset + (5 * xPos)));

            return pixels;
        }

        public List<Pixel> GetL(int xOffset)
        {
            List<Pixel> pixels = new List<Pixel>();

            pixels.AddRange(AddSpace(xOffset - xPos));

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var p = new Pixel(width, height, xOffset + i * xPos, 10 + j * yPos);

                    if (i == 0 || j == 4)
                    {
                        p.col = paintColor;
                    }
                    else
                    {
                        p.col = Color.White;
                    }

                    pixels.Add(p);
                }

            }

            pixels.AddRange(AddSpace(xOffset + (5 * xPos)));

            return pixels;
        }

        public List<Pixel> AddSpace(int xPos)
        {
            List<Pixel> pixels = new List<Pixel>();

            for (int j = 0; j < 5; j++)
            {
                var p = new Pixel(width, height, xPos, 10 + j * yPos);

                p.col = Color.White;

                pixels.Add(p);
            }

            return pixels;
        }

        public void OpenColorPicker()
        {
            ColorDialog colorPicker = new ColorDialog();
            // Keeps the user from selecting a custom color.
            colorPicker.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            colorPicker.ShowHelp = true;
            // Sets the initial color select to the current text color.
            colorPicker.Color = paintColor;

            // Update the text box color if the user clicks OK 
            if (colorPicker.ShowDialog() == DialogResult.OK)
                paintColor = colorPicker.Color;
        }
    }
}