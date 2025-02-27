using System;
using System.Linq;
using System.Windows.Forms;
using Airport.Data;
using Airport.Models;
using Microsoft.EntityFrameworkCore;

namespace Airport.Operator
{
    public partial class InfoPass : Form
    {
        public InfoPass()
        {
            InitializeComponent();
        }

        private void InfoPass_Load(object sender, EventArgs e)
        {
            LoadPassengerData();
        }

        private void LoadPassengerData()
        {
            using (AirportDbContext db = new AirportDbContext())
            {
                var passengerData = db.Passengers
                    .Include(p => p.Flight) // Загрузка данных о рейсе
                    .Include(p => p.Baggage) // Загрузка данных о багаже
                    .Select(p => new
                    {
                        FullName = $"{p.Name} {p.Surname}",
                        BirthDate = p.BirthDate.ToShortDateString(),
                        FlightNumber = p.Flight.FlightNumber,
                        DepartureAirport = p.Flight.DepartureAirport,
                        ArrivalAirport = p.Flight.ArrivalAirport,
                        BaggageWeight = p.Baggage != null ? $"{p.Baggage.Weight} кг" : "Нет",
                        IsRegistered = p.IsReg ? "Да" : "Нет" // Проверка на регистрацию
                    })
                    .ToList();

                dataGridView1.Rows.Clear();

                foreach (var passenger in passengerData)
                {
                    dataGridView1.Rows.Add(
                        passenger.FullName,
                        passenger.BirthDate,
                        passenger.FlightNumber,
                        passenger.DepartureAirport,
                        passenger.ArrivalAirport,
                        passenger.BaggageWeight,
                        passenger.IsRegistered // Добавление информации о регистрации
                    );
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }

}
