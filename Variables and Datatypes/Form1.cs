using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Variables_and_Datatypes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int firstNumber;
            int secondNumber;
            int answer;

            firstNumber = int.Parse(textBox1.Text);
            secondNumber = int.Parse(textBox2.Text);

            answer = firstNumber + secondNumber;
            label3.Text = answer.ToString();
        }
    }
}
