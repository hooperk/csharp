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
    /// <summary>
    /// Form for adding new courses
    /// </summary>
    public partial class NewCourses : Form
    {
        private List<Entry> entries;
        /// <summary>
        /// Timer for resetting feedback label
        /// </summary>
        private Timer eventTimer;
        private static readonly String LabelText = "Enter course details and hit \"Add Course\"";
        private static readonly char[] Currency = { '£', '$', '€' };

        /// <summary>
        /// New Form, reset timer
        /// </summary>
        public NewCourses()
        {
            InitializeComponent();
            entries = new List<Entry>();
            eventTimer = new Timer();
            eventTimer.Interval = 5000;
            eventTimer.Tick += resetEvent;
        }

        //When an item is selected from the list, set textbox to it
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = entries[listBox1.SelectedIndex].Name;
        }

        /// <summary>
        /// Shows the form as a modal dialog box
        /// </summary>
        /// <returns>Entries this produces</returns>
        public new IEnumerable<Entry> ShowDialog()
        {
            base.ShowDialog();
            return entries;
        }

        /// <summary>
        /// Display an entry as a single line
        /// </summary>
        /// <param name="entry">Entry to display</param>
        /// <returns>Name, Date: Price</returns>
        String DisplayEntry(Entry entry)
        {
            return entry.Name + ", " + entry.GetDate() + ": " + entry.Price;
        }

        //Add Course button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String price = textBox3.Text;
                if (!Currency.Any(c => c == price[0]))
                    price = "€" + textBox3.Text;//make sure a currency symbol is present, € as defaults
                Entry newEntry = new Entry(textBox1.Text, textBox2.Text, price);
                entries.Add(newEntry);
                listBox1.Items.Add(DisplayEntry(newEntry));
                textBox1.Text = textBox2.Text = textBox3.Text = "";//chain empty string assignment, make all boxes empty
                SetLabel("Course Added");
            }
            catch(FormatException)
            {
                SetLabel("Invalid Date");
            }
        }

        /// <summary>
        /// Set the feedback label and start a timer to return it to normal
        /// </summary>
        /// <param name="label">The temporary text for feedback</param>
        void SetLabel(String label)
        {
            label4.Text = label;
            eventTimer.Enabled = true;
            eventTimer.Start();
        }

        //event that resets the label
        private void resetEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                /* Not on UI thread, reenter there... */
                this.BeginInvoke(new EventHandler(resetEvent), sender, e);
            }
            else
            {
                lock (eventTimer)
                {
                    /* only work when this is no reentry while we are already working */
                    if (eventTimer.Enabled)
                    {
                        eventTimer.Stop();
                        label4.Text = LabelText;
                        eventTimer.Enabled = false;
                    }
                }
            }
        }

        //close button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
