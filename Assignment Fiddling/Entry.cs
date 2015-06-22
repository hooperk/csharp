using System;
using System.Text;

namespace Assignment_Fiddling
{
    struct Entry
    {
        String Name { get; set; }
        DateTime Date { get; set; }
        String Price { get; set; }
        Boolean[] Booked { get; set; }

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
            output.Append("\"");
            return output.ToString();
        }
    }
}
