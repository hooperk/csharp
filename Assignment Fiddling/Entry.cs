using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment_Fiddling
{
    public class Entry
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public String Price { get; set; }
        public Boolean[] Booked { get; set; }

        public Entry(String name, String date, String price, String booked)
        {
            Name = name.Replace("\"","");
            Date = DateTime.Parse(date.Replace("\"", ""));
            Price = price.Replace("\"", "");
            Booked = new Boolean[12];
            Char[] seats = booked.Replace("\"", "").ToCharArray();
            for (int i = 0; i < 12; i++)
                Booked[i] = (seats[i] == 'B');
        }

        public String GetDate()
        {
            return Date.ToString("d");
        }

        public override String ToString(){
            StringBuilder output = new StringBuilder("\"" + Name + "\"" + Environment.NewLine + "\"" + GetDate() + "\""
                + Environment.NewLine + "\"" + Price + "\"" + Environment.NewLine + "\"");
            foreach (Boolean b in Booked)
                output.Append(b ? "B" : "F");
            output.Append("\"" + Environment.NewLine);
            return output.ToString();
        }

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
                while ((line = reader.ReadLine()) != null)
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
   
    }

    public static class EntryExtension{
        public static String GetAllEntries(this IEnumerable<Entry> self)
        {
            StringBuilder output = new StringBuilder("");
            foreach (Entry e in self)
                output.Append(e.ToString());
            return output.ToString();
        }
    }
}
