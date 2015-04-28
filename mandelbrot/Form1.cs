using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mandelbrot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap I;
        private double zx, zy, cX, cY, tmp;
        private Color c;
        private void button1_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            int m, derinlik; double yakınlık;
            if (!int.TryParse(textBox1.Text, out m) || !int.TryParse(textBox2.Text, out m) || textBox1.Text == "" || textBox2.Text == "") { derinlik = 1000; yakınlık = 150; }
            else
            {
                derinlik = Convert.ToInt32(textBox1.Text);
                yakınlık = Convert.ToInt32(textBox2.Text);
            }
            int genişlik = pictureBox1.Width;
            int yükseklik = pictureBox1.Height;
            I = new Bitmap(genişlik, yükseklik);
            for (int y = 0; y < yükseklik; y++)
            {                 //
                for (int x = 0; x < genişlik; x++)
                {              // Tüm pikseller için
                    zx = zy = 0;
                    cX = (x/(float)genişlik)*3.5-2;                          //x koordinatını orta nokta yapıyor
                    cY = (y/(float)yükseklik)*2.5-1.25;                          //y koordinatını orta nokta yapıyor
                    int iter = derinlik;                            //en fazla yapılacak iterasyon (derinlik)
                    while (zx * zx + zy * zy < 4 && iter > 0)
                    {     //x^2+y^2 < 2^2 ve iterasyon > 0 ise dön
                        tmp = zx * zx - zy * zy + cX;               // 
                        zy = 2.0 * zx * zy + cY;                    // zy = 2*zx*zy+cy
                        zx = tmp;                                   // zx = zx^2-zy^2+cx
                        iter--;                                     //iterasyon azalmış
                    }
                    iter = iter | (iter << 8); iter = iter >> 8;
                    c = Color.FromArgb((iter << 8)%256,(iter << 4)%256,(iter)%256);
                    I.SetPixel(x, y, c);             //iterasyon sayısı ile aterasyonun 8 bit kaydırılmış halini orlamış
                }
            }
            pictureBox1.Image = I;
            MessageBox.Show(sw.ElapsedMilliseconds.ToString() + " Milisaniye ");
            sw.Stop();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
