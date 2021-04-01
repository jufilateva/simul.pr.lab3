using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Flight ball = new Flight();
        private void flStart_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();

            ball.y0 = (double)flUDHeight.Value;
            ball.v0 = (double)flUDSpeed.Value;
            ball.a = (double)flUDAngle.Value;
            ball.s = (double)flUDArea.Value;
            ball.m = (double)flUDWeight.Value;
            
            
            
            chart1.Series[0].Points.AddXY(ball.x, ball.y);

            ball.v();
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            ball.major();
           
            chart1.Series[0].Points.AddXY(ball.x, ball.y);
            if (ball.y <= 0) timer1.Stop();
            time.Text = "Time: " + ball.t;
        }

        private void pause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void play_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
    public class Flight
    {
        const double dt = 0.01;
        const double g = 9.81;
        const double c = 0.15;
        const double rho = 1.29;
        public double y = 0, t = 0, x = 0;
        public double y0, v0, a, m, s, k, vx, vy;

        public void v()
        {
            vx = v0 * Math.Cos(a * Math.PI / 180);
            vy = v0 * Math.Sin(a * Math.PI / 180);

            k = 0.5 * c * s * rho / m;
            x = 0;
            t = 0;
            y = y0;

        }

        public void major()
        {
            t += dt;

            vx = vx - k * vx * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vy - (g + k * vy * Math.Sqrt(vx * vx + vy * vy)) * dt;

            x = x + vx * dt;
            y = y + vy * dt;
        }
    }

}
