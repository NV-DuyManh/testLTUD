namespace nhom
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class RapEntities : DbContext
    {
        public RapEntities()
            : base("name=RapEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        // ĐÂY CHÍNH LÀ ĐOẠN VISUAL STUDIO BỎ QUÊN NÀY BẠN:
        public virtual DbSet<ghe> ghes { get; set; }
        public virtual DbSet<lichchieu> lichchieus { get; set; }
        public virtual DbSet<nguoidung> nguoidungs { get; set; }
        public virtual DbSet<phim> phims { get; set; }
        public virtual DbSet<phongchieu> phongchieus { get; set; }
        public virtual DbSet<sanpham> sanphams { get; set; }
        public virtual DbSet<theloai> theloais { get; set; }
    }
}