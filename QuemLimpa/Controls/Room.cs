using QuemLimpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuemLimpa.Controls
{
    public class Room
    {
        private List<Colaborador> colaboradores { get; set; }
        public List<(string[] cleaners, DateTime day)> Cleaners { get; private set; } = new List<(string[] cleaners, DateTime day)>();
        public int CleanerQuantity { get; private set; }

        public Room(int cleanerQtd)
        {
            colaboradores = new List<Colaborador>();

            string[] nomes = new string[]
            {
                "Manu",
                "Gabi",
                "Iago",
                "Thiago",
                "Maitê",
                "Bruno",
                "Carioca",
                "Kaiky",
                "Fortunato",
                "Felipe",
                "Hemerson",
                "André",
                "Leonardo",
                "Freire",
                "Ramirez",
                "Murilo"
            };

            if (nomes.Length < cleanerQtd)
                throw new ArgumentException("Quantidade de limpadores invalida");

            this.CleanerQuantity = cleanerQtd;

            foreach (var nome in nomes)
                colaboradores.Add(new Colaborador() { Name = nome });

            DayCleanGenerator(new DateTime(2023, 3, 16));
            GetWeekCleaners(new DateTime(2023, 4, 15));
        }

        public void DayCleanGenerator(DateTime initialDate = default)
        {
            Cleaners.Clear();

            if (initialDate == default) initialDate = DateTime.Today.PreventWeekend();

            int skip = 0;
            while (Cleaners.Count < 365)
            {
                string[] c = new string[this.CleanerQuantity];

                for (int i = skip, j = 0; i < skip + this.CleanerQuantity && j < this.CleanerQuantity; i++, j++)
                {
                    if (skip == colaboradores.Count || i == colaboradores.Count)
                    {
                        skip = -j;
                        i = 0;
                    }

                    c[j] = colaboradores[i].Name;
                }

                Cleaners.Add((c, initialDate));

                do
                {
                    initialDate = initialDate.AddDays(1);
                }
                while ((int)initialDate.DayOfWeek > 5 || (int)initialDate.DayOfWeek < 1);


                skip += this.CleanerQuantity;
            }
        }


        public List<(string[], DateTime)> GetWeekCleaners(DateTime day = default)
        {
            var weekList = new List<(string[], DateTime)>();

            if (day == default) day = DateTime.Today;

            int weekDay = (int)day.DayOfWeek - 1;
            DateTime aux = default;

            for (int i = 0; i < 5; i++)
            {
                aux = day.AddDays(-weekDay + i);
                var query = Cleaners.First(l => l.day == aux);
                weekList.Add(query);
                Console.WriteLine(aux);
            }

            return weekList;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime PreventWeekend(this DateTime d)
        {
            while ((int)d.DayOfWeek > 5 || (int)d.DayOfWeek < 1)
                d = d.AddDays(1);
            return d;
        }
    }
}