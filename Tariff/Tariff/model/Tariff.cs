using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tariff
{
    [Serializable]
    class Tariff : IReadOnlyTariff
    {
        private static int _id;

        public int Id { get; private set; }
        public double Price { get; private set; }
        public string Name { get; private set; }
        public int Minutes { get; private set; }
        public int Gygabytes { get; private set; }
        public int Messages { get; private set; }

        static Tariff()
        {
            _id = 0;
        }

        public Tariff(string name, int minutes, int gygabytes, int messages, int price)
        {
            Id = ++_id;
            Minutes = minutes;
            Gygabytes = gygabytes;
            Messages = messages;
            Price = price;
            Name = name;
        }

        public override string ToString() => Name; 
    }

    public interface IReadOnlyTariff
    {
        int Id { get; }
        double Price { get; }
        string Name { get; }
        int Minutes { get; }
        int Gygabytes { get; }
        int Messages { get; }
    }
}
