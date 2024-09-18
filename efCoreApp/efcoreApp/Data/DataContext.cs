using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext: DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)//db context optionsı gönderiyor.
        {
            
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
         public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();
        
    }
}

//code first yaklaşımıyla database oluşturuyoruz.(entity,dbContext => Database )
//database first : Sql i açıp manuel olarak veritabanını oluşturma.