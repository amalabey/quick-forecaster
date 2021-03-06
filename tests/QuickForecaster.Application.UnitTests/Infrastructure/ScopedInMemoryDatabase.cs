﻿using QuickForecaster.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickForecaster.Application.UnitTests.Infrastructure
{
    public class ScopedInmemoryDatabase : IDisposable
    {
        public QuickForecasterDbContext Context { get; private set; }

        public string DatabaseName { get; private set; }

        public ScopedInmemoryDatabase(QuickForecasterDbContext context, string dbName)
        {
            Context = context;
            DatabaseName = dbName;
        }

        public void Dispose()
        {
            if(Context != null)
            {
                Context.Database.EnsureDeleted();
                Context.Dispose();
            }            
        }
    }
}
