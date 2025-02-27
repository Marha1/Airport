    using System;

    namespace Airport.Models
    {
        public class Baggage
        {
            // Уникальный идентификатор багажа
            public int Id { get; set; }

            // Вес багажа в килограммах
            public double Weight { get; set; }

            // Размеры багажа (длина, ширина, высота) в сантиметрах
            public double Length { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            // Связь с пассажиром
            public int PassengerId { get; set; } // Внешний ключ на пассажира
            public Passenger Passenger { get; set; } // Навигационное свойство

        }
    }
