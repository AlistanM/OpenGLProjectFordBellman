using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openglgraphlastversion
{
    public partial class Form1 : Form
    {
        private GraphPainter _painter;
        private GraphControl _control;
        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();
            _painter = new GraphPainter(simpleOpenGlControl1);
            _control = new GraphControl(_painter, label5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _painter.Clear();
            string txt = textBox1.Text;
            _control.Solve(txt, textBox3.Text, textBox4.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _painter.Clear();
            string txt = textBox1.Text;
            _control.DrawUnsolved(txt, textBox2.Text);
        }
    }
}
