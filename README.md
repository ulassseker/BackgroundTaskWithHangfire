# BackgroundTaskWithHangfire
An ASP.NET Core 3.0 WEB API Project with Hangfire for Background Task Scheduling and MongoDB for updating db after scheduled jobs  

  ## Some Technologies and Packages
- ASP.NET Core App 3.0
- Entity Framework Core 3.0 (For incoming api json file)
- Entity Framework Core CodeFirst Migrations (for the first db initialization)
- MSSQL Server (For incoming api json file)
- Hangfire 1.7.9 (for Background Task Scheduling)
- MongoDB.Driver 2.10.1 (a document based NoSQL db for updating db after Job executings)
- MongoDB Compass (for db visualization)


 ## Notes
- Background (recurring) task is working every 2 hours start from 00:00 for every day
- You can activate the recurring task either triggering manually in /hangfire dashboard or calling the /api/Information page
- If you want to run task with the same time of running project, you can call hangfire class in the very end of startup.cs file 
- MongoDB Compass is optional. You can do the same job with the help of a command shell 
- All connections are local, mongodb has also free cloud server named MongoDB Atlas, this is also an option

## References
- https://docs.microsoft.com/tr-tr/aspnet/core/tutorials/first-mongo-app
- https://zellwk.com/blog/local-mongodb/
- https://mitchelsellers.com/blog/article/implementing-background-tasks-in-asp-net-core-with-hangfire
