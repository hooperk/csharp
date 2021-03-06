﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment_Fiddling
{
    public partial class Form1 : Form
    {
        private Entry[] entries;
        public Form1()
        {
            InitializeComponent();
            entries = new Entry[10];
            entries[0] = new Entry("C# Basics", "01/02/2015", "€300", "BFFBFFFBFFFF");
            entries[1] = new Entry("C# Basics", "03/02/2015", "€300.00", "BBBBFFFFFFFF");
            entries[2] = new Entry("C# Basics", "02/02/2015", "€499.50", "FFFFFFFFFFFF");
            entries[3] = new Entry("C# Basics", "04/02/2015", "€499", "FFFBFFFBFFBB");
            entries[4] = new Entry("C# Basics", "05/02/2015", "€2000", "BFFBFFFBFFFF");
            entries[5] = new Entry("C# Basics", "11/02/2015", "€256", "BFFBFFFBFFFF");
            entries[6] = new Entry("C# Basics", "21/03/2015", "€300", "BFFBFFFBFFFF");
            entries[7] = new Entry("C# Basics", "01/03/2015", "€190", "FFFBFFFBFFFF");
            entries[8] = new Entry("C# Basics", "1/4/2015", "€300", "BFFBFFFBFFFF");
            entries[9] = new Entry("C# Basics", "01/02/2016", "€305", "FFFFFFFFFFFF");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form auto = new AutoGen();
            auto.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form dynamic = new BookingPlan(entries.Where(entry => entry.Name == entries.First().Name));
            dynamic.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog newFile = new SaveFileDialog();
            newFile.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            newFile.FilterIndex = 1;
            newFile.DefaultExt = ".txt";
            newFile.FileName = "Courses.txt";
            if (newFile.ShowDialog() == DialogResult.OK)
            {
                ClassGenerator generator = new ClassGenerator();
                entries = generator.Generate((int)numericUpDown1.Value);
                entries.SaveFile(newFile.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Course_Booker_9001 form = new Course_Booker_9001();
            form.ShowDialog();
        }
    }
}
