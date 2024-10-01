using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pupils
{
    public partial class NguyenADO : DbContext
    {
        public NguyenADO()
            : base("name=NguyenADO")
        {
        }

        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<sysdiagram>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
