using Airport.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airport.User
{
    public partial class СheckInfoUser : Form
    {
        public СheckInfoUser()
        {
            InitializeComponent();
        }

        private void СheckInfoUser_Load(object sender, EventArgs e)
        {
            Getflight();
        }
        private void Getflight()
        {
            using (AirportDbContext db = new AirportDbContext())
            {
                var flights = db.Flights.OrderBy(x => x.Id);
                // Очищаем DataGridView
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                // Добавление колонок в DataGridView
                dataGridView1.Columns.Add("Number", "Номер рейса");
                dataGridView1.Columns.Add("StartingPoint", "Точка отправки");
                dataGridView1.Columns.Add("EndPoint", "Точка прибытия");
                dataGridView1.Columns.Add("Status", "Статус");
                dataGridView1.Columns.Add("DispatchDate", "Время Отправки");
                dataGridView1.Columns.Add("ArrivalDate", "Время приыбытия");

                // Заполнение DataGridView данными из базы
                foreach (var flight in flights)
                {

                    dataGridView1.Rows.Add(
                        flight.FlightNumber,
                        flight.DepartureAirport,
                        flight.ArrivalAirport,
                        flight.Status,
                        flight.DepartureTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm"),
                        flight.ArrivalTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
                    );
                }

            }
        }
    }
}
