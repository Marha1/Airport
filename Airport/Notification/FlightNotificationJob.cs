using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Quartz;
using Quartz.Impl;
using Airport.Data;
using Airport.Notification;
using Airport.Primitives;

public class FlightNotificationJob : IJob
{
    private readonly AirportDbContext _context;
    private readonly Notificat _notificat;

    public FlightNotificationJob(AirportDbContext context, Notificat notificat)
    {
        _context = context;
        _notificat = notificat;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var flightId = context.JobDetail.JobDataMap.GetInt("FlightId");
            Debug.WriteLine($"Executing job for FlightId: {flightId} at {DateTime.Now}");

            var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);

            if (flight == null)
            {
                Debug.WriteLine("Flight not found.");
                return;
            }

            Debug.WriteLine($"Flight {flight.FlightNumber} found with status: {flight.Status}");

            if (flight.Status != FlightStatus.BoardingOfPassengers)
            {
                Debug.WriteLine("Flight is not in BoardingOfPassengers status. Skipping...");
                return;
            }

            var unregisteredPassengers = _context.Passengers
                .Where(p => p.FlightId == flightId && !p.IsReg)
                .Select(p => $"{p.Name} {p.Surname} {p.Lastname}")
                .ToList();

            if (unregisteredPassengers.Any())
            {
                var message = $"Пассажиры, следующие на рейс {flight.FlightNumber}: " +
                              string.Join(", ", unregisteredPassengers) +
                              ". Пожалуйста, завершите регистрацию.";

                Debug.WriteLine($"Notification: {message}");

                await _notificat.PlayMusic();
                await _notificat.Zader(message);
            }
            else
            {
                Debug.WriteLine($"All passengers for flight {flight.FlightNumber} are registered.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in job execution: {ex.Message}");
        }
    }
}
