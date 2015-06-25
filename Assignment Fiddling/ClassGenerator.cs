using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Fiddling
{
    class ClassGenerator
    {
        Tuple<String, List<DateTime>>[] classes;//store used dates for classes
        Random gen;

        public ClassGenerator()
        {
            classes = new Tuple<string,  List<DateTime>>[15];
            classes[0] = new Tuple<string, List<DateTime>>("C# Basics", new List<DateTime>());
            classes[1] = new Tuple<string, List<DateTime>>("C# Advanced", new List<DateTime>());
            classes[2] = new Tuple<string, List<DateTime>>("C++ Beginners", new List<DateTime>());
            classes[3] = new Tuple<string, List<DateTime>>("C++ Advanced", new List<DateTime>());
            classes[4] = new Tuple<string, List<DateTime>>("Pascal Advanced", new List<DateTime>());
            classes[5] = new Tuple<string, List<DateTime>>("Pascal Beginners", new List<DateTime>());
            classes[6] = new Tuple<string, List<DateTime>>("Visual Basic Beginners", new List<DateTime>());
            classes[7] = new Tuple<string, List<DateTime>>("Visual Basic Advanced", new List<DateTime>());
            classes[8] = new Tuple<string, List<DateTime>>("Web Design", new List<DateTime>());
            classes[9] = new Tuple<string, List<DateTime>>("Javascript Basics", new List<DateTime>());
            classes[10] = new Tuple<string, List<DateTime>>("JQuery", new List<DateTime>());
            classes[11] = new Tuple<string, List<DateTime>>("Angular JS", new  List<DateTime>());
            classes[12] = new Tuple<string, List<DateTime>>("Getting started with Code Academy", new List<DateTime>());
            classes[13] = new Tuple<string, List<DateTime>>("SQL Basics", new List<DateTime>());
            classes[14] = new Tuple<string, List<DateTime>>("SQL Advanced", new List<DateTime>());
            reset();
        }

        /// <summary>
        /// Clear all the used dates, reset random number generator
        /// </summary>
        void reset()
        {
            for (int i = 0; i < 15; i++)
                classes[i].Item2.Clear();
            gen = new Random();
        }

        /// <summary>
        /// Generate a new list of classes
        /// </summary>
        /// <param name="size">number of classes to create</param>
        /// <returns>An array of classes</returns>
        public Entry[] Generate(int size)
        {
            Entry[] entries = new Entry[size];
            Entry current = null;
            int choice = 0;
            DateTime date;
            decimal price = 0;
            char[] booked = new char[12];
            for(int i = 0; i < size; i++){
                do
                {
                    choice = gen.Next(15);
                } while (classes[choice].Item2.Count == 10);//if the selected class already has 10 entries, pick another
                do
                {
                    date = RandomDay();
                } while (classes[choice].Item2.Contains(date));//make random dates until you find an unused one
                classes[choice].Item2.Add(date);
                price = 0.05m * (4000 + gen.Next(12001));//€200 - €800
                bool filled = gen.NextDouble() < 0.4;//60% chance to be empty
                for (int j = 0; j < 12; j++)
                {
                    booked[j] = (filled && gen.NextDouble() > 0.5 ? 'B' : 'F');//if empty, Free, else 50% chance of either
                }
                current = new Entry(
                    Entry.Wrap(classes[choice].Item1),
                    Entry.Wrap(date.ToString("d")),
                    Entry.Wrap(price.ToString("C")),
                    Entry.Wrap(new String(booked))
                );
                entries[i] = current;
            }
            return entries;
        }

        /// <summary>
        /// Choose a random date
        /// </summary>
        /// <returns>Random date in the period august 2015 to december 2016</returns>
        DateTime RandomDay()
        {
            DateTime start = new DateTime(2015, 8, 1);

            int range = (new DateTime(2016, 12,31) - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
