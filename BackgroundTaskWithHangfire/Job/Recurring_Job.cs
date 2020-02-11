using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackgroundTaskWithHangfire.Data;
using BackgroundTaskWithHangfire.Models;
using BackgroundTaskWithHangfire.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace BackgroundTaskWithHangfire.Job
{
    public class Recurring_Job
    {
        private readonly BackgroundTaskWithHangfireContext _context;
        private readonly InformationMongoService _informationMongoService;
        public Recurring_Job(BackgroundTaskWithHangfireContext context, InformationMongoService informationMongoService)
        {
            _context = context;
            _informationMongoService = informationMongoService;

            //Recurring job
            var crnExp = "0 0 0/2 1/1 * ?"; //2 saatte 1 çalışacak iş için oluşturulan cron expression
            RecurringJob.AddOrUpdate(() => ProcessRecurringJob(),cronExpression: crnExp);
        }

        public void ProcessRecurringJob()
        {
            _ = SaveInstancesToDb();
        }

        public async Task SaveInstancesToDb()
        {
            var information = await _context.Information.ToListAsync();

            var informationForMongo = new InformationMongo();
            foreach(var item in information)
            {
                informationForMongo.Title = item.title;
                informationForMongo.Description = item.description;
                informationForMongo.Filename = item.filename;
                informationForMongo.Height = item.height;
                informationForMongo.Width = item.width;
                informationForMongo.Type = item.type;
                informationForMongo.Price = item.price;
                informationForMongo.Rating = item.rating;

                _informationMongoService.Create(informationForMongo);
            }
        }
    }
}
