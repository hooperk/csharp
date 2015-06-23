using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_Fiddling
{
    class BookingPlan : Form
    {
        private static Color selected = Color.FromArgb(0, 255, 0);
        
        private Entry[] classes;
        private String lecture = "";

        /// <summary>
        /// Create the Booking Plan form from an array of classes
        /// </summary>
        /// <param name="entries">List of entries in booking form</param>
        public BookingPlan(Entry[] entries)
        {
            classes = entries;
            if (classes != null && classes.Length > 0)
                this.lecture = classes[0].Name;
            InitializeComponent(classes.Length);
        }

        /// <summary>
        /// Delegate method for all the seat button's click methods
        /// </summary>
        /// <param name="sender">The Button</param>
        /// <param name="e">Eventargs</param>
        private void seat_Click(object sender, EventArgs e)
        {
            Button target = sender as Button;
            if (target != null)
            {
                int id = int.Parse(target.Name.Substring(4));
                int row = id / 12;//button 1-12 is 0-11 which is all 0 here
                int button = id % 12;//same again, will just have to add 1 when putting back to string
                classes[row].Booked[button] = !classes[row].Booked[button];
                Toggle(target, classes[row].Booked[button], button);
            }
        }

        /// <summary>
        /// single method for togglign button for use in generation loop as well
        /// </summary>
        /// <param name="self">button to toggle</param>
        /// <param name="set">Should it be set</param>
        /// <param name="id">id for button to show if unset</param>
        private static void Toggle(Button self, bool set, int id)
        {
            if (set)
            {
                Set(self);
            }
            else
            {
                Unset(self, id);
            }
        }

        /// <summary>
        /// Unset a button
        /// </summary>
        /// <param name="self">The Button</param>
        /// <param name="id">button number to set</param>
        public static void Unset(Button self, int id)
        {
            self.UseVisualStyleBackColor = true;
            self.Text = (id+1).ToString();
        }

        /// <summary>
        /// Set a button
        /// </summary>
        /// <param name="self">The Button</param>
        private static void Set(Button self)
        {
            self.UseVisualStyleBackColor = false;
            self.Text = "B";
        }

        /// <summary>
        /// Delegate for print all onclick
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">Eventargs</param>
        private void print_all(object sender, EventArgs e)
        {
            //Print all booking forms
        }

        /// <summary>
        /// Close method for close button
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">Eventargs</param>
        private void close_click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Draw the fields, rows are 0 indexed
        /// </summary>
        /// <param name="rows">number of rows to be added</param>
        private void InitializeComponent(int rows)
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(191, 255, 199);
            this.ClientSize = new System.Drawing.Size(559, 418);
            this.Name = "Book Seats";
            this.Text = "Book Seats - " + lecture;
            #region Headers
            // 
            // label1
            // 
            this.label1 = new System.Windows.Forms.Label();
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course Name:";
            this.Controls.Add(this.label1);
            // 
            // label2
            // 
            this.label2 = new System.Windows.Forms.Label();
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = lecture;
            this.Controls.Add(this.label2);
            // 
            // label3
            // 
            this.label3 = new System.Windows.Forms.Label();
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(501, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cost Per Person";
            this.Controls.Add(this.label3);
            // 
            // label4
            // 
            this.label4 = new System.Windows.Forms.Label();
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(440, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Date";
            this.Controls.Add(this.label4);
            #endregion
            #region rows
            //for each row, for each button, then spit out labels
            //initialise storage for rows
            rowLabels = new List<Label>(rows * 2);
            rowButtons = new List<Button>(rows * 12);
            int count = 5;//tabindex of items
            for (int i = 0; i < rows; i++)
            {
                //12 buttons
                for (int j = 0; j < 12; j++)
                {
                    Button latest = new System.Windows.Forms.Button();
                    latest.Location = new System.Drawing.Point(10 + (j*34), 62 + (i*29));
                    latest.Name = "seat" + ((i*12)+j).ToString();
                    latest.Size = new System.Drawing.Size(28, 23);
                    latest.TabIndex = count++;//increment count after each button
                    latest.BackColor = selected;
                    Toggle(latest, classes[i].Booked[j], j);
                    latest.Click += seat_Click;
                    rowButtons.Add(latest);
                    this.Controls.Add(latest);
                }
                //date
                Label date = new System.Windows.Forms.Label();
                date.AutoSize = false;
                date.Location = new System.Drawing.Point(422, 67 + (i*29));
                date.Name = "date" + i;
                date.Size = new System.Drawing.Size(51, 13);
                date.TabIndex = count++;
                date.Text = classes[i].GetDate();
                date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                rowLabels.Add(date);
                this.Controls.Add(date);
                //cost per person
                Label price = new System.Windows.Forms.Label();
                price.AutoSize = false;
                price.Location = new System.Drawing.Point(501, 67 + (i*29));
                price.Name = "price" + i;
                price.Size = new System.Drawing.Size(51, 13);
                price.TabIndex = count++;
                price.Text = classes[i].Price;
                rowLabels.Add(price);
                this.Controls.Add(price);
            }
            #endregion
                #region Constant Buttons
                // 
                // button121
                // 
                this.button121 = new System.Windows.Forms.Button();
            this.button121.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button121.Location = new System.Drawing.Point(310, 356);
            this.button121.Name = "button121";
            this.button121.Size = new System.Drawing.Size(102, 56);
            this.button121.TabIndex = 144;
            this.button121.Text = "Print All Booking Forms";
            this.button121.UseVisualStyleBackColor = true;
            this.button121.Click += print_all;
            this.Controls.Add(this.button121);
            // 
            // button122
            // 
            this.button122 = new System.Windows.Forms.Button();
            this.button122.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button122.Location = new System.Drawing.Point(485, 356);
            this.button122.Name = "button122";
            this.button122.Size = new System.Drawing.Size(62, 56);
            this.button122.TabIndex = 145;
            this.button122.Text = "Close Form";
            this.button122.UseVisualStyleBackColor = true;
            this.button122.Click += close_click;
            this.Controls.Add(this.button122);
            #endregion
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private List<System.Windows.Forms.Label> rowLabels;
        private List<System.Windows.Forms.Button> rowButtons;
        private System.Windows.Forms.Button button121;
        private System.Windows.Forms.Button button122;
    }
}
