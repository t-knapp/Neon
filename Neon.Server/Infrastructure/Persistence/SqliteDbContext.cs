using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Neon.Infrastructure;

public class SqliteDbContext : ApplicationDbContext
{
    public SqliteDbContext( DbContextOptions<SqliteDbContext> options ) : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
    }
}