namespace Pupils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SINHVIEN")]
    public partial class SINHVIEN
    {
        [Key]
        [StringLength(13)]
        public string MSSV { get; set; }

        [StringLength(50)]
        public string HOTEN { get; set; }

        [StringLength(30)]
        public string KHOA { get; set; }

        public double? DIEMTB { get; set; }
    }
}
