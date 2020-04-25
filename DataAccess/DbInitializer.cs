using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HawaiiCrimeDetails.DataAccess;
using HawaiiCrimeDetails.APIHandlerManager;
using HawaiiCrimeDetails.Models;
using System.Collections.Generic;

namespace HawaiiCrimeDetails.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            APIHandler webHandler = new APIHandler();
            var dataList = webHandler.GetData();

            foreach (RootData a in dataList)
            {
                var validIds = dbContext.data.Select(t => t.objectid).ToList();
                if (!validIds.Contains(a.objectid))
                    dbContext.data.Add(a);
            }

            dbContext.SaveChanges();

        }
    }
}