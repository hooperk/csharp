using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment
{
    public class Entry
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public String Price { get; set; }
        public Boolean[] Booked { get; set; }

        /// <summary>
        /// Create a new Course from the 4 lines
        /// </summary>
        /// <param name="name">Name of the course</param>
        /// <param name="date">Date of the course</param>
        /// <param name="price">Cost for the Course</param>
        /// <param name="booked">Available seats</param>
        public Entry(String name, String date, String price, String booked)
        {
            Name = name.Trim('"');
            Date = DateTime.Parse(date.Trim('"'));
            Price = price.Trim('"');
            Booked = new Boolean[12];
            Char[] seats = booked.Trim('"').ToCharArray();
            for (int i = 0; i < 12; i++)
                Booked[i] = (seats[i] == 'B');
        }

        /// <summary>
        /// Wrapper to get the date in dd/mm/yyyy format
        /// </summary>
        /// <returns>dd/mm/yyyy of the course</returns>
        public String GetDate()
        {
            return Date.ToString("d");
        }

        /// <summary>
        /// Returns the entry as a single strign of 4 lines
        /// </summary>
        /// <returns>Name, Date, Price and booked seats on 1 line each</returns>
        public override String ToString(){
            StringBuilder output = new StringBuilder(Wrap(Name) + Environment.NewLine + Wrap(GetDate())
                + Environment.NewLine + Wrap(Price) + Environment.NewLine + "\"");
            foreach (Boolean b in Booked)
                output.Append(b ? "B" : "F");
            output.Append("\"" + Environment.NewLine);
            return output.ToString();
        }

        /// <summary>
        /// Parse a file containing the 4 lines of entries
        /// </summary>
        /// <param name="target">Stream to the file</param>
        /// <returns>Array of all the entries present</returns>
        public static Entry[] OpenFile(FileStream target)
        {
            String line = null;
            List<Entry> entries = new List<Entry>();
            Entry entry = null;
            String name;
            String date;
            String price;
            String booked;
            using(StreamReader reader = new StreamReader(target))
            {
                while (!String.IsNullOrWhiteSpace(line = reader.ReadLine()))//last line may be only empty or just a newline/space
                {
                    name = line;
                    if ((line = reader.ReadLine()) == null)
                        throw new BookingException(1);
                    date = line;
                    if ((line = reader.ReadLine()) == null)
                        throw new BookingException(1);
                    price = line;
                    if ((line = reader.ReadLine()) == null || line.Length != 14)
                        throw new BookingException(1);
                    booked = line;
                    try
                    {
                        entry = new Entry(name, date, price, booked);
                        entries.Add(entry);
                    }
                    catch (FormatException e)
                    {
                        throw new BookingException(4, e.Message, e);
                    }
                    catch (ArgumentNullException e)
                    {
                        throw new BookingException(1, e.Message, e);
                    }
                }
            }
            if (entries.Count == 0)
                throw new BookingException(1);
            return entries.ToArray();
        }

        /// <summary>
        /// Save the entries to a file
        /// </summary>
        /// <param name="self">Entry enumeration</param>
        /// <param name="path">path to save to</param>
        public static void SaveFile(IEnumerable<Entry> self, String path)
        {
            self.SaveFile(path);
        }

        /// <summary>
        /// Wrap a line in quotation marks
        /// </summary>
        /// <param name="input">string to wrap</param>
        /// <returns>A quoted string</returns>
        public static String Wrap(String input)
        {
            return "\"" + input + "\"";
        }
   
    }

    public static class EntryExtension{
        /// <summary>
        /// Convert the Entry Enumeration to a single string
        /// </summary>
        /// <param name="self"></param>
        /// <returns>String ready to output to </returns>
        public static String GetAllEntries(this IEnumerable<Entry> self)
        {
            StringBuilder output = new StringBuilder("");
            foreach (Entry e in self)
                output.Append(e.ToString());
            return output.ToString();
        }

        /// <summary>
        /// Save the entries to a file
        /// </summary>
        /// <param name="self">Entry enumeration</param>
        /// <param name="path">path to save to</param>
        public static void SaveFile(this IEnumerable<Entry> self, String path)
        {
            File.WriteAllText(path, self.GetAllEntries());
            //using (FileStream location = File.Create(path))
            //using (StreamWriter writer = new StreamWriter(location))
            //{
            //    writer.Write(self.GetAllEntries());
            //}
        }
    }
}
