using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Assignment
{
    public partial class Course_Booker_9001 : Form
    {
        private IEnumerable<Entry> entries;
        private IEnumerable<String> names;
        private string defaultSave = "Courses.txt";

        public Course_Booker_9001()
        {
            InitializeComponent();
            listBox1.DoubleClick += list_DoubleClick;
        }

        //new
        private void menuItem2_Click(object sender, EventArgs e)
        {

        }

        //open
        private void menuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = File.OpenRead(openDialog.FileName))
                {
                    try
                    {
                        entries = Entry.OpenFile(stream);
                        label1.Text = "Courses:";
                        names = entries.Select(entry => entry.Name).Distinct();
                        listBox1.BeginUpdate();
                        listBox1.Items.Clear();
                        foreach (String name in names)
                            listBox1.Items.Add(name);
                        listBox1.EndUpdate();
                        defaultSave = Path.GetFileName(openDialog.FileName);
                    }
                    catch (BookingException ex)
                    {
                        label1.Text = ex.ErrorMessage();
                    }
                }
            }
        }

        //save
        private void menuItem4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.DefaultExt = ".txt";
            saveDialog.FileName = defaultSave;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                entries.SaveFile(saveDialog.FileName);
            }
        }

        //exit
        private void menuItem6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                String name = list.SelectedItem.ToString();
                using (BookingPlan booker = new BookingPlan(entries.Where(entry => entry.Name == name)))
                {
                    booker.ShowDialog();
                }
            }
        }
    }
}
