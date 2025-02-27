using Airport.Data;
using Airport.Models.Airport.Models;
using Airport.Notification;
using Airport.Primitives;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airport.Operator
{
    public partial class CheckFly : Form
    {
        private FlightStatus _flightstatus;
        private Flight _current;
        private Notificat notificat = new Notificat();
        private string _number;
        public CheckFly()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void CheckFly_Load(object sender, EventArgs e)
        {
            Getflight();
            var flightStatus = Enum.GetValues(typeof(FlightStatus));
            comboBox1.DataSource = flightStatus;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {

                using (AirportDbContext _context = new AirportDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string Name = selectedRow.Cells["Number"].Value.ToString();
                    string startpoint = selectedRow.Cells["StartingPoint"].Value.ToString();
                    string endpoint = selectedRow.Cells["EndPoint"].Value.ToString();
                    var flight = _context.Flights.Include(X => X.Passengers).Where(s =>
            s.FlightNumber == Name &&
            s.DepartureAirport == startpoint &&
            s.ArrivalAirport == endpoint);

                    if (flight != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Произошла Ошибка!");
                    }

                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите Рейс.");
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {
                using (AirportDbContext _context = new AirportDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    _flightstatus = (FlightStatus)comboBox1.SelectedItem;
                    string Name = selectedRow.Cells["Number"].Value.ToString();
                    string startpoint = selectedRow.Cells["StartingPoint"].Value.ToString();
                    string endpoint = selectedRow.Cells["EndPoint"].Value.ToString();
                    var flight = _context.Flights.FirstOrDefault(s =>
            s.FlightNumber == Name &&
            s.DepartureAirport == startpoint &&
            s.ArrivalAirport == endpoint);

                    if (flight != null)
                    {
                        _current = flight;
                        switch (_flightstatus)
                        {


                            case FlightStatus.Preparation:
                                flight.Status = _flightstatus;
                                _context.SaveChanges();
                                break;
                            case FlightStatus.BoardingOfPassengers:
                                await notificat.PlayMusic();
                                await notificat.OnBoarding(flight.FlightNumber);
                                flight.Status = _flightstatus;
                                _context.SaveChanges();
                                DateTime localTime = flight.DepartureTime.ToLocalTime();
                                ///Добавить задачу,за 15 мин до конца отлета озвучить кто не прошел регистрацию
                                var schedulerService = new SchedulerService();
                                Console.WriteLine("Scheduling task...");
                                await schedulerService.ScheduleFlightNotification(flight.Id, flight.DepartureTime.ToLocalTime());
                                Console.WriteLine("Task scheduled.");
                                break;
                            case FlightStatus.Flight:
                                break;
                            case FlightStatus.Detained:
                                _number = flight.FlightNumber;
                                label2.Visible = true;
                                numericUpDown1.Visible = true;
                                button2.Visible = true;
                                break;
                            case FlightStatus.Canceled:
                                break;
                            default:
                                break;
                        }
                        Getflight();
                    }
                    else
                    {
                        MessageBox.Show("Произошла Ошибка!");
                    }

                }


            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите Рейс.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            using (AirportDbContext _context = new AirportDbContext())
            {


                _current.DepartureTime = _current.DepartureTime.AddHours((int)numericUpDown1.Value);
                _current.ArrivalTime = _current.ArrivalTime.AddHours((int)numericUpDown1.Value);
                _current.Status = _flightstatus;


                await notificat.PlayMusic();
                await notificat.Perenos(_number, numericUpDown1.Value.ToString());
                button2.Visible = false;
                label2.Visible = false;
                numericUpDown1.Visible = false;
                _context.Update(_current);
                _context.SaveChanges();
                Getflight();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if(dataGridView1.SelectedRows.Count > 0) // Проверяем, есть ли выделенная строка
            {

                using (AirportDbContext _context = new AirportDbContext())
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];


                    // Получаем данные из выбранной строки
                    string Name = selectedRow.Cells["Number"].Value.ToString();
                    string startpoint = selectedRow.Cells["StartingPoint"].Value.ToString();
                    string endpoint = selectedRow.Cells["EndPoint"].Value.ToString();
                    var flight = _context.Flights.FirstOrDefault(s =>
            s.FlightNumber == Name &&
            s.DepartureAirport == startpoint &&
            s.ArrivalAirport == endpoint);

                    if (flight != null)
                    {
                        RegForm regForm = new RegForm(flight.Id);
                        regForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Произошла Ошибка!");
                    }

                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите Рейс.");
            }

        }
    }
}
