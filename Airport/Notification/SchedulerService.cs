using Quartz.Impl;
using Quartz;
using System.Diagnostics;

public class SchedulerService
{
    private readonly IScheduler _scheduler;

    public SchedulerService()
    {
        _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

        if (!_scheduler.IsStarted)
        {
            Debug.WriteLine("Планировщик не запущен, выполняется запуск...");
            _scheduler.Start();
        }
        else
        {
            Debug.WriteLine("Планировщик уже работает.");
        }
    }

    public async Task ScheduleFlightNotification(int flightId, DateTime departureTime)
    {
        var jobKey = new JobKey($"FlightNotification_{flightId}", "FlightNotifications");

        if (await _scheduler.CheckExists(jobKey))
        {
            Debug.WriteLine($"Задача для рейса {flightId} уже существует в планировщике.");
            return;
        }

        var job = JobBuilder.Create<FlightNotificationJob>()
            .WithIdentity(jobKey)
            .UsingJobData("FlightId", flightId)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity($"Trigger_FlightNotification_{flightId}", "FlightNotifications")
            .StartAt(departureTime.AddMinutes(-1))
            .Build();

        await _scheduler.ScheduleJob(job, trigger);

        Debug.WriteLine($"Задача для рейса {flightId} запланирована на {departureTime.AddMinutes(-1)}.");
    }
}
