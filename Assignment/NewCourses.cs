using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class NewCourses : Form
    {
        private List<Entry> entries;

        public NewCourses()
        {
            InitializeComponent();
            entries = new List<Entry>();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set textbox1.Text = entries[selectedIndex];
        }

        public IEnumerable<Entry> ShowDialog()
        {
            base.ShowDialog();
            return entries;
        }
    }
}
