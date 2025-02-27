using System;

namespace Airport.ValueObject
{
    public abstract class Passport
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; } = string.Empty;

        // Поле для хранения даты рождения
        private DateTime _birthDate {  get; set; }

        // Свойство для задания даты рождения
        public DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        // Автоматический расчет возраста
        public int Age
        {
            get
            {
                DateTime today = DateTime.UtcNow;
                int age = today.Year - _birthDate.Year;

                // Корректировка возраста, если день рождения ещё не наступил в этом году
                if (_birthDate > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }
        public string PassNumber { get; set; }
    }
}
