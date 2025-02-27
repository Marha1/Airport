using System;
using System.Linq;
using System.Windows.Forms;
using Airport.Data;
using Airport.Models;

namespace Airport.Operator
{
    public partial class RegForm : Form
    {
        private int _currentFlightId;

        public RegForm(int flightId)
        {
            InitializeComponent();
            _currentFlightId = flightId;
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (_currentFlightId != 0)
            {
                try
                {
                    // Создание объекта Passenger
                    Passenger passenger = new Passenger
                    {
                        Name = txtName.Text.Trim(),
                        Surname = txtSurname.Text.Trim(),
                        Lastname = txtLastname.Text.Trim(),
                        BirthDate = dtpBirthDate.Value,
                        PassNumber = txtPassportNumber.Text.Trim(),
                        FlightId = _currentFlightId,
                        IsReg = true, // Устанавливаем статус регистрации
                        Baggage = new Baggage
                        {
                            Weight = (double)nudBaggageWeight.Value,
                            Length = double.TryParse(txtLength.Text.Trim(), out var length) ? length : 0,
                            Width = double.TryParse(txtWidth.Text.Trim(), out var width) ? width : 0,
                            Height = double.TryParse(txtHeight.Text.Trim(), out var height) ? height : 0
                        }
                    };

                    // Сохранение или обновление данных пассажира
                    RegisterPassenger(passenger);

                    MessageBox.Show("Пассажир успешно зарегистрирован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Очистка полей ввода
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при регистрации пассажира: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Рейс не назначен");
            }
        }

        private void RegisterPassenger(Passenger passenger)
        {
            using (var context = new AirportDbContext()) // Используйте ваш DbContext
            {
                // Проверка, существует ли уже пассажир с такими данными
                var existingPassenger = context.Passengers
                    .FirstOrDefault(p =>
                        p.PassNumber == passenger.PassNumber &&
                        p.Name == passenger.Name &&
                        p.Surname == passenger.Surname &&
                        p.BirthDate == passenger.BirthDate);

                if (existingPassenger != null)
                {
                    // Если пассажир уже существует, обновляем его статус и багаж
                    existingPassenger.IsReg = true;
                    existingPassenger.FlightId = _currentFlightId;

                    if (existingPassenger.Baggage == null)
                    {
                        existingPassenger.Baggage = new Baggage();
                    }

                    existingPassenger.Baggage.Weight = passenger.Baggage.Weight;
                    existingPassenger.Baggage.Length = passenger.Baggage.Length;
                    existingPassenger.Baggage.Width = passenger.Baggage.Width;
                    existingPassenger.Baggage.Height = passenger.Baggage.Height;

                    context.SaveChanges();
                }
                else
                {
                    // Если пассажир не существует, добавляем нового
                    context.Passengers.Add(passenger);
                    context.SaveChanges();
                }
            }
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtLastname.Clear();
            txtPassportNumber.Clear();
            dtpBirthDate.Value = DateTime.Now;
            nudBaggageWeight.Value = 0;
            txtLength.Clear();
            txtWidth.Clear();
            txtHeight.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            CheckFly checkFly = new CheckFly();
            checkFly.Show();
        }
    }
}