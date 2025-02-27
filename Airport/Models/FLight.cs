using Airport.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    using System;
    using System.Collections.Generic;

    namespace Airport.Models
    {
        public class Flight
        {
            // Уникальный идентификатор рейса
            public int Id { get; set; }

            // Номер рейса
            public string FlightNumber { get; set; }

            // Аэропорты отправления и прибытия
            public string DepartureAirport { get; set; }
            public string ArrivalAirport { get; set; }

            // Время отправления и прибытия
            private DateTime _DepartureTime { get; set; }

            // Свойство для задания даты рождения
            public DateTime DepartureTime
            {
                get => _DepartureTime;
                set => _DepartureTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }


            private DateTime _ArrivalTime { get; set; }

            // Свойство для задания даты рождения
            public DateTime ArrivalTime
            {
                get => _ArrivalTime;
                set => _ArrivalTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            }

            // Статус рейса
            public FlightStatus Status { get; set; }

            public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
        }
    }
}
