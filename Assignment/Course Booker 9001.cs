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
    /// <summary>
    /// Main form for booking courses
    /// </summary>
    public partial class Course_Booker_9001 : Form
    {
        private IEnumerable<Entry> entries;
        private IEnumerable<String> names;
        
        private string defaultSave = "Courses.txt";

        /// <summary>
        /// Create new Course Booker form
        /// </summary>
        public Course_Booker_9001()
        {
            InitializeComponent();
            listBox1.DoubleClick += list_DoubleClick;
        }

        //new
        private void menuItem2_Click(object sender, EventArgs e)
        {
            NewCourses courseForm = new NewCourses();
            entries = courseForm.ShowDialog();
            ResetListbox();
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
                        ResetListbox();
                        defaultSave = Path.GetFileName(openDialog.FileName);
                    }
                    catch (BookingException ex)
                    {
                        label1.Text = ex.ErrorMessage();
                    }
                }
            }
        }

        //Method which reloads the list of courses in the listbox
        private void ResetListbox()
        {
            label1.Text = "Courses:";
            names = entries.Select(entry => entry.Name).Distinct();
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (String name in names)
                listBox1.Items.Add(name);
            listBox1.EndUpdate();
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

        //open the booking plan form upon doubleclick
        private void list_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1 != null)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    //Check that an item was actually selected
                    Rectangle selected = listBox1.GetItemRectangle(listBox1.SelectedIndex);
                    if (selected.Contains(listBox1.PointToClient(Cursor.Position)))
                    {
                        String name = listBox1.SelectedItem.ToString();
                        using (BookingPlan booker = new BookingPlan(entries.Where(entry => entry.Name == name)))
                        {
                            booker.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
