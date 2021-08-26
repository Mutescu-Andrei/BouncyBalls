using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;

namespace _9__Problema_25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            InitializeComponent();
        }

        
        private RectangleF[] BilaXYZW;
        private Vector[] VelocitateBila;
        private Size FormSize;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Bile 
            Random rand = new Random();
            const int NrBile = 5;
            BilaXYZW = new RectangleF[NrBile];
            VelocitateBila = new Vector[NrBile];
            for (int i = 0; i < NrBile; i++)
            {
                int width = rand.Next(10, 40);
                BilaXYZW[i] = new RectangleF(rand.Next(0, ClientSize.Width -  2*width),rand.Next(0, ClientSize.Height - 2* width), width, width);
                int v1 = rand.Next(2, 20);
                int v2 = rand.Next(2, 20);
             
                VelocitateBila[i] = new Vector(v1, v2,0);
            }

            // Canvas la fel
            FormSize = ClientSize;

            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // ca sa nu "flicker-easca",  https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-reduce-graphics-flicker-with-double-buffering-for-forms-and-controls
        }

        private void misca(object sender, EventArgs e)
        {
            for (int i = 0; i < BilaXYZW.Length; i++)//luam bilele si tot le mutam
            {

                double x2 = BilaXYZW[i].X + VelocitateBila[i].X;
                double y2 = BilaXYZW[i].Y + VelocitateBila[i].Y;
                
                 VelocitateBila[i].X = VelocitateBila[i].X *0.98;
                VelocitateBila[i].Y = VelocitateBila[i].Y *0.98;
             
                if (x2 < 0)
                {
                    VelocitateBila[i].X = -VelocitateBila[i].X;

                }
                else if (x2 + BilaXYZW[i].Width > FormSize.Width)
                {
                    VelocitateBila[i].X = -VelocitateBila[i].X;

                }
                if (y2 < 0)
                {
                    VelocitateBila[i].Y = -VelocitateBila[i].Y;

                }
                else if (y2 + BilaXYZW[i].Height > FormSize.Height)
                {
                    VelocitateBila[i].Y = -VelocitateBila[i].Y;

                }
                float x3 = (float)x2;
                float y3 = (float)y2;
                BilaXYZW[i] = new RectangleF(x3, y3,BilaXYZW[i].Width,BilaXYZW[i].Height); //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawellipse?view=dotnet-plat-ext-3.1
            }
            Coliziune();
            Refresh();
        }

        private void Coliziune()
        {
            Vector Velocitate = new Vector();
            bool[] coliz = new bool[50];
            bool[] coliz2 = new bool[50];
            
            for (int i = 0; i < BilaXYZW.Length ; i++)
            {
                for (int j = 0; j< BilaXYZW.Length; j++)
                {if (i != j)
                    {
                        coliz[i] = Izbire(BilaXYZW[i], BilaXYZW[j]);
                        coliz2[j] = Izbire(BilaXYZW[j], BilaXYZW[i]);
                        if (coliz[i]==true || coliz2[j]==true)
                        {
                            Velocitate = schimbaVEL(VelocitateBila[i], BilaXYZW[i], VelocitateBila[j], BilaXYZW[j]);
                            VelocitateBila[j] = schimbaVEL(VelocitateBila[j], BilaXYZW[j], VelocitateBila[i], BilaXYZW[i]);
                            VelocitateBila[i] = Velocitate;

                        }
                    }
                }
            }
           
        }

        private Vector schimbaVEL(Vector vel1, RectangleF bila1, Vector vel2, RectangleF bila2)
        {
            Vector centru = new Vector();
            centru.X = bila1.X/2 - bila2.X/2;
            centru.Y = bila1.Y/2 - bila2.Y/2;
            centru.Z = 0;
           
            Vector bila1perp = centru.Perpendiculara(vel1);
            Vector bila2perp = centru.Perpendiculara(vel2);

            //Vector3 bila1pro = centru.Proiectie(vel1);
            Vector bila2pro = centru.Proiectie(vel2);

            Vector bila1NOUAvelocitate = bila2pro + bila1perp; 

            return bila1NOUAvelocitate*1.01; //velocitatea primei bile
        }

        private bool Izbire(RectangleF bila, RectangleF bila2)
        {
            Vector distantabile=new Vector();
               distantabile.X = bila.X/2.0 - bila2.X/2.0;
            distantabile.Y = bila.Y/2.0 - bila2.Y/2.0;
            distantabile.Z = 0;
            Vector raza = new Vector();
            raza.X = bila.X + bila2.X;
            raza.Y = bila.Y + bila2.Y;
            raza.Z = 0;
            float distanta = (float)distantabile.Dim();
          //  float coliziune = (float)(raza.X*raza.Y);  
            if (distanta <25) //diametru ca sa nu se intersecteze
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        // desenam
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Brush[] brushes = new Brush[] {Brushes.Green, Brushes.DarkGreen,Brushes.DarkOliveGreen,Brushes.ForestGreen,Brushes.GreenYellow};

            Random rnd = new Random();
            

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
           
            for (int i = 0; i < BilaXYZW.Length; i++)
            {
                Brush brush = brushes[rnd.Next(brushes.Length)];



                e.Graphics.DrawEllipse(Pens.Black, BilaXYZW[i]);
                e.Graphics.FillEllipse(brush, BilaXYZW[i]);
                
            }
        }
    }
}
