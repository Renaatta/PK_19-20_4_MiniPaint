﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.CSharp;
//using System.Deployment;
//using System.Net.Http;
//using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Point tmpPoint;
        Pen myPen;
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            myPen = new Pen(buttonColor.BackColor, (float)numericUpDownWidth.Value);
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            nowyToolStripMenuItem_Click(null, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxMyImage.Image = new Bitmap(800, 600);
            graphics = Graphics.FromImage(pictureBoxMyImage.Image);
            graphics.Clear(Color.White);
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxMyImage.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBoxMyImage.Image);
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
                ImageFormat imageFormat = ImageFormat.Bmp;
                switch (extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;

                }
                pictureBoxMyImage.Image.Save(saveFileDialog.FileName, imageFormat);
            }
        }

        private void pictureBoxMyImage_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                tmpPoint = e.Location;
            }
        }

        private void pictureBoxMyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tmpPoint, e.Location);
                    pictureBoxMyImage.Refresh();
                    tmpPoint = e.Location;
                }
            }
        }

        private void pictureBoxMyImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tmpPoint, e.Location);
                }
                else if (radioButtonLine.Checked)
                {
                    graphics.DrawLine(myPen, tmpPoint, e.Location);
                }
                else if (radioButtonRectangle.Checked)
                {
                    graphics.DrawRectangle(myPen, 
                                           Math.Min(tmpPoint.X, e.X), 
                                           Math.Min(tmpPoint.Y, e.Y),
                                           Math.Abs(tmpPoint.X - e.X),
                                           Math.Abs(tmpPoint.Y - e.Y));
                }
                else if (radioButtonElipse.Checked)
                {
                    graphics.DrawEllipse(myPen,
                                           Math.Min(tmpPoint.X, e.X),
                                           Math.Min(tmpPoint.Y, e.Y),
                                           Math.Abs(tmpPoint.X - e.X),
                                           Math.Abs(tmpPoint.Y - e.Y));
                }
                pictureBoxMyImage.Refresh();
            }
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = (float)numericUpDownWidth.Value;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog.Color;
                myPen.Color = colorDialog.Color;
            }
        }
    }
}
