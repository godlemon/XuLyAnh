using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void layanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ops = new OpenFileDialog();
            if (ops.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(ops.FileName);
                no1pic.Image = bmp;
                info.Text = "Width: " + bmp.Width + ", Height: " + bmp.Height;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            string lik = "D:\\hoctap\\Xulyanh\\tuan1\\Bai1\\ImageData.txt";

            StreamWriter sw = new StreamWriter(lik);
            Bitmap bmp = (Bitmap)no1pic.Image;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    sw.Write("(" + c.R + "," + c.G + "," + c.B + ")");

                }
                sw.WriteLine();
            }
            sw.Close();
            MessageBox.Show("Thông tin đã được chuyển đến file ImageData.txt tại " + lik, "Thành công");
        }
        private void white_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            for (int y = 100; y < 500; ++y)
            {
                for (int x = 100; x < 900; ++x)
                {
                    Color c = Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(x, y, c);
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau khi Bôi trắng";
        }

        private void giR_Click(object sender, EventArgs e)
        {
            string lik = "D:\\hoctap\\Xulyanh\\tuan1\\Bai1\\HiG.txt";
            StreamWriter sw = new StreamWriter(lik);
            Bitmap bmp = (Bitmap)no1pic.Image;
            int[] hist = new int[256];
            //hist.Initialize();
            for (int i = 0; i < 256; ++i)
                hist[i] = 0;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.R]++;
                }
            }
            for (int i = 0; i < 256; ++i)
                sw.WriteLine(i + ": " + hist[i]);
            sw.Close();
            MessageBox.Show("Thông tin đã được chuyển đến file HiG.txt tại "+ lik,"Thành công");
        }

        private void Tang_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(nhap.Text);
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x,y);
                    int r = c.R + t;
                    int g = c.G + t;
                    int b = c.B + t;
                    if(r>255) r = 255;
                    if(g>255) g = 255;
                    if(b>255) b = 255;
                    if(r<0) r = 0;
                    if(g<0) g = 0;
                    if(b<0) b = 0;
                    Color h = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, h);
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau khi Thay đổi độ sáng";
        }


        private void amban_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    int r = 255 - c.R;
                    int g = 255 - c.G;
                    int b = 255 - c.B;
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    if (r < 0) r = 0;
                    if (g < 0) g = 0;
                    if (b < 0) b = 0;
                    Color h = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, h);
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau khi chuyển sang âm bản";
        }

        public void canbangR(Bitmap bmp, int nelv)
        {
            int tb = (bmp.Height * bmp.Width) / nelv;
            int[] hist = new int[256];
            int[] tg = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            tg.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.R]++;
                }
            }
            tg[0] = hist[0];
            for (int i = 1; i < 256; i++)
            {
                    tg[i] = tg[i-1] + hist[i];
            }
            for (int i = 0; i < 256; i++)
            {
                fg[i] = (tg[i] / tb) - 1;
                fg[i] = Convert.ToInt32(Math.Round(Convert.ToDouble(fg[i])));
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    int r = fg[c.R];
                    if (r < 0) r = 0;
                    Color h = Color.FromArgb(r, r, r);
                    bmp.SetPixel(x, y, h);
                }
            }
        }
        public void canbangG(Bitmap bmp, int nelv)
        {
            double tb = (bmp.Height * bmp.Width) / nelv;
            int[] hist = new int[256];
            int[] tg = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            tg.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.B]++;
                }
            }
            tg[0] = hist[0];
            for (int i = 1; i < 256; i++)
            {
                tg[i] = tg[i - 1] + hist[i];
            }
            for (int i = 0; i < 256; i++)
            {
                fg[i] = (int)Math.Round((tg[i] / tb) - 1);
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    int b = fg[c.G];
                    if (b < 0) b = 0;
                    Color h = Color.FromArgb(b, b, b);
                    bmp.SetPixel(x, y, h);
                }
            }
        }
        public void canbangB(Bitmap bmp, int nelv)
        {
            double tb = (bmp.Height * bmp.Width) / nelv;
            int[] hist = new int[256];
            int[] tg = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            tg.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.B]++;
                }
            }
            tg[0] = hist[0];
            for (int i = 1; i < 256; i++)
            {
                    tg[i] = tg[i-1] + hist[i];
            }
            for (int i = 0; i < 256; i++)
            {
                fg[i] = (int)Math.Round((tg[i] / tb) - 1);
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    int b = fg[c.B];
                    if (b < 0) b = 0;
                    Color h = Color.FromArgb(b, b, b);      
                }
            }
        }
        private void canbang_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(nhap.Text) ;
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            Byte rgb;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    rgb = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                    bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            canbangR(bmp, n);
            canbangG(bmp, n); 
            canbangB(bmp, n);
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau khi cân bằng";
        }

        private void bawi_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            Byte rgb;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    rgb = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                    bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh đen trắng";
        }

        private void sa_Click(object sender, EventArgs e)
        {
            string lik = "D:\\hoctap\\Xulyanh\\tuan1\\Bai1\\ketqua.jpg";
            Bitmap bmp = (Bitmap)no2pic.Image;
            if (bmp != null)
            {
                bmp.Save(lik);
                MessageBox.Show("Thông tin đã được chuyển đến file ketqua.jpg tại " + lik, "Thành công");
            }
            else
                MessageBox.Show("Lỗi, không có ảnh");
        }
        public void gianR(Bitmap bmp,int l)
        {
            int gmin = 256;
            int gmax = 0;
            int[] hist = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.R]++;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                {
                    gmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; --i)
            {
                if (hist[i] != 0)
                {
                    gmax = i;
                    break;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                    fg[i] = (l - 1) * ((i - gmin) / (gmax - gmin));
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (fg[c.R] != 0) 
                    {
                        int r = fg[c.R];
                        Color h = Color.FromArgb(r, c.G, c.B);

                        bmp.SetPixel(x, y, h);
                    }
                }
            }
        }
        public void gianG(Bitmap bmp, int l)
        {
            int gmin = 256;
            int gmax = 0;
            int[] hist = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.G]++;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                {
                    gmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; --i)
            {
                if (hist[i] != 0)
                {
                    gmax = i;
                    break;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                    fg[i] = (l - 1) * ((i - gmin) / (gmax - gmin));
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (fg[c.G] != 0)
                    {
                        int g = fg[c.G];
                        Color h = Color.FromArgb(c.R, g, c.B);

                        bmp.SetPixel(x, y, h);
                    }

                }
            }
        }
        public void gianB(Bitmap bmp, int l)
        {
            int gmin = 256;
            int gmax = 0;
            int[] hist = new int[256];
            int[] fg = new int[256];
            hist.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    hist[c.B]++;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                {
                    gmin = i;
                    break;
                }
            }
            for (int i = 255; i >= 0; --i)
            {
                if (hist[i] != 0)
                {
                    gmax = i;
                    break;
                }
            }
            for (int i = 0; i < 256; ++i)
            {
                if (hist[i] != 0)
                    fg[i] = (l - 1) * ((i - gmin) / (gmax - gmin));
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (fg[c.B] != 0)
                    {
                        int b = fg[c.B];
                        Color h = Color.FromArgb(c.R, c.G, b);
                        bmp.SetPixel(x, y, h);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int l = Convert.ToInt32(nhap.Text);
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();

            gianR(bmp,l);
            gianG(bmp,l);
            gianB(bmp,l);
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau khi Giãn";
        }

        private int kt(int value)
        {
            return Math.Min(255, Math.Max(0, value));
        }

        public void tp(Bitmap bmp, double a, double b, double A, double B, double G)
        {
            
            double va = a * A;
            double vb = B * (b - a) + va;

            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    //r
                    int r = 0;
                    if (c.R <= a)
                        r = (int)(A * c.R);
                    else if (c.R > a && c.R <= b)
                        r = (int)(B * (c.R - a) + va);
                    else if (c.R > b)
                        r = (int)(G * (c.R - b) + vb);
                    r = kt(r);
                    //g
                    int g = 0;
                    if (c.G <= a)
                        g = (int)(A * c.G);
                    else if (c.G > a && c.G <= b)
                        g = (int)(B * (c.G - a) + va);
                    else if (c.G > b)
                        g = (int)(G * (c.G - b) + vb);
                    g = kt(g);
                    //b
                    int bt = 0;
                    if (c.B <= a)
                        bt = (int)(A * c.B);
                    else if (c.B > a && c.B <= b)
                        bt = (int)(B * (c.B - a) + va);
                    else if (c.B > b)
                        bt = (int)(G * (c.B - b) + vb);
                    bt = kt(bt);
                    Color h = Color.FromArgb(r, g, bt);
                    bmp.SetPixel(x, y, h);
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh sau Thay độ tương phản";
        }

        private void tuongphan_Click(object sender, EventArgs e)
        {
            Form2 hehe = new Form2();
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            
            hehe.ShowDialog();
            tp(bmp, hehe.ayl(), hehe.byl(), hehe.Ayl(), hehe.Byl(), hehe.Gyl());
        }

        private void tachnguong_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image.Clone();
            int min = 0;
            int max = 255;
            int Q = Convert.ToInt32(nhap.Text);
            Byte rgb;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    Color c = bmp.GetPixel(x, y);
                    rgb = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                    bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    int r = 0, g = 0, b = 0;
                    Color c = bmp.GetPixel(x, y);
                    if (c.R >= Q) {  r = max; }
                    else if (c.R < Q) {  r = min; }
                    if (c.G >= Q) {  g = max; }
                    else if (c.G < Q) {  g = min; }
                    if (c.B >= Q) {  b = max; }
                    else if (c.B < Q) {  b = min; }
                    Color h = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, h);
                }
            }
            no2pic.Image = bmp;
            thongbao2.Text = "Ảnh ";
        }

        private void tachnguong2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image;
            int r = 0, g = 0, b = 0;
            int[] his = new int[256];
            int[] tg = new int[256];
            int[] ghg = new int[256];
            int[] tong = new int[256];
            double[] mg = new double[256];
            double[] fg = new double[256];
            his.Initialize();
            tg.Initialize();
            ghg.Initialize();
            tong.Initialize();
            mg.Initialize();
            fg.Initialize();
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    his[c.R]++;
                }
            }
            int size = bmp.Width * bmp.Height;
            tg[0] = his[0];
            for (int i = 1; i < 256; i++)
            {
                tg[i] = tg[i - 1] + his[i];
            }
            for (int i = 0; i < 256; i++)
            {
                ghg[i] = i * his[i];
            }
            tong[0] = ghg[0];
            for (int i = 1; i < 256; i++)
            {
                tong[i] = tong[i - 1] + ghg[i];
            }
            for (int i = 0; i < 256; i++)
            {
                mg[i] = tong[i] / tg[i];
            }
            for (int i = 0; i < 256; i++)
            {
                int t = size - tg[i];
                if (t != 0)
                {
                    fg[i] = (tg[i] / t) * (mg[i] - mg[255]) * (mg[i] - mg[255]);
                }
            }
            double max = 0;
            int theta = 0;
            for (int i = 0; i < 256; i++)
            {
                if (fg[i] > max)
                {
                    max = fg[i];
                    theta = i;
                }
            }
            test.Text = theta.ToString();
            int maxi = 255;
            int min = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    if (c.R >= theta) r = maxi;
                    else r = min;

                    if (c.G >= theta) g = maxi;
                    else g = min;

                    if (c.B >= theta) b = maxi;
                    else b = min;
                    Color z = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, z);
                }
            }
            no2pic.Image = bmp;
        }

        private void nhanchap_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image;
            int m = 2, n = 2;
            double[,] r = { { 0.5, 1.25 }, 
                            { 0.15, 0 } };
            
            for (int y = 0; y < bmp.Height-1; y++)//Đứng
            {
                for (int x = 0; x < bmp.Width-1; x++)//ngang
                {
                    double sumR = 0, sumG = 0, sumB = 0;
                    for(int i = 0; i < m; i++)
                    {
                        for(int j = 0; j < n; j++)
                        {
                            Color g = bmp.GetPixel(x, y);
                            sumR += r[i, j] * g.R;
                            sumB += r[i, j] * g.B;
                            sumG += r[i, j] * g.G;
                        }
                    }          
                    //i.B * a + j.B * b + k.B * c + l.B * d;
                    if(sumR < 0) { sumR = 0; }
                    if(sumG < 0) { sumG = 0; }
                    if(sumB < 0) { sumB = 0; }
                    if(sumR > 255) { sumR = 255; }
                    if(sumG > 255) { sumG = 255; }
                    if(sumB > 255) { sumB = 255; }
                    Color z = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                    bmp.SetPixel(x, y, z);
                }    
            }
            no2pic.Image = bmp;
        }

        private void trungvi_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image;
            int m = 3, n = m;
            int theta = 3;
            List<int> demR= new List<int>();
            List<int> demG = new List<int>();
            List<int> demB = new List<int>();
            for (int y = 0; y < bmp.Height; y++)//Đứng
            {
                for (int x = 0; x < bmp.Width; x++)//ngang
                {
                    Color g = bmp.GetPixel(x, y);
                    if (y == 0 || x == 0|| x == bmp.Width -1 || y == bmp.Height - 1)
                    {
                        
                        bmp.SetPixel(x, y, g);
                    }
                    else
                    {
                        int no = 4;
                        double sumR = g.R, sumG = g.G, sumB = g.B;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color h = bmp.GetPixel(x + j, y + i);
                                demR.Add(h.R);
                                demG.Add(h.G);
                                demB.Add(h.B);
                            }
                        }
                        demR.Sort();
                        demG.Sort();
                        demB.Sort();
                        if (Math.Abs(g.R - demR[no]) > theta)
                            sumR = demR[no];
                        if (Math.Abs(g.G - demG[no]) > theta)
                            sumG = demG[no];
                        if (Math.Abs(g.B - demB[no]) > theta)
                            sumB = demB[no];
                        Color z = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                        bmp.SetPixel(x, y, z);
                        demR.Clear();
                        demG.Clear();
                        demB.Clear();
                    }
                    
                }
            }
            no2pic.Image = bmp;
        }

        private void trungbinh_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)no1pic.Image;
            int theta = 3;
            for (int y = 0; y < bmp.Height; y++)//Đứng
            {
                for (int x = 0; x < bmp.Width; x++)//ngang
                {
                    Color g = bmp.GetPixel(x, y);
                    if (y == 0 || x == 0 || x == bmp.Width - 1 || y == bmp.Height - 1)
                    {
                        bmp.SetPixel(x, y, g);
                    }
                    else
                    {
                        int demR = 0, demG = 0, demB = 0;
                        double sumR = g.R, sumG = g.G, sumB = g.B;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                Color h = bmp.GetPixel(x + j, y + i);
                                demR += h.R;
                                demG += h.G;
                                demB += h.B;
                            }
                        }
                        if (Math.Abs(g.R - demR / 9) > theta)
                            sumR = (int)(demR / 9);
                        if (Math.Abs(g.G - demG / 9) > theta)
                            sumG = (int)(demG / 9);
                        if (Math.Abs(g.B - demB / 9) > theta)
                            sumB = (int)(demB / 9);
                        Color z = Color.FromArgb((int)sumR, (int)sumG, (int)sumB);
                        bmp.SetPixel(x, y, z);
                    }

                }
            }
            no2pic.Image = bmp;
        }
    }
}

