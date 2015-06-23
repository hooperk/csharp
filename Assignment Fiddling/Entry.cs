using System;
using System.Collections.Generic;
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
            for (int i = 0; i < 12 && i < seats.Length; i++)
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
