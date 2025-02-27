using Airport.Models.Airport.Models;
using Airport.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Passenger : Passport
    {
        public int Id { get; set; }
    
        public bool IsReg { get; set; }

        // Связь с рейсом
        public int FlightId { get; set; } // Внешний ключ на рейс
        public Flight Flight { get; set; } // Навигационное свойство
        public Baggage Baggage { get; set; } // Навигационное свойство (1 пассажир имеет 1 багаж)

    }
}
