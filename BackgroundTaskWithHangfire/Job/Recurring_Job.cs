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

            //Recurring job activation
            var crnExp = "0 0 0/2 1/1 * ?"; //cron expression which works for every 2 hours 
            RecurringJob.AddOrUpdate(() => ProcessRecurringJob(),cronExpression: crnExp);
        }

        public async Task ProcessRecurringJob()
        {
            await SaveInstancesToDb();
        }

        public async Task SaveInstancesToDb()
        {
            //first get all information from db or /api/information 
            var information = await _context.Information.ToListAsync();

            //convert it to mongo based bson format and create new instance for each object
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
                informationForMongo.Id = null;

                _informationMongoService.Create(informationForMongo);
            }
        }
    }
}
