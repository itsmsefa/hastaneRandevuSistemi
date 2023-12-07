using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hastaneRandevuSistemi.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
        public DbSet<Hastane> Poliklinikler => Set<Hastane>();
        public DbSet<Il> Iller => Set<Il>();
        public DbSet<Birim> Birimler => Set<Birim>();
        public DbSet<Doktor> Doktorlar => Set<Doktor>();
        public DbSet<Hasta> Hastalar => Set<Hasta>();
        public DbSet<Randevu> Randevular => Set<Randevu>();
    }
}