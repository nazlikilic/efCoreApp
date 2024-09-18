using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string AdSoyad { 
            get 
            {
                return this.Ad + " " + this.Soyad;
            } 
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        [DataType(DataType.Date)]//date time ile default olarak gelen saat bilgisini istemiyorsak saat bilgisini çıkarıp sadece tarih bilgisinin kalması için kullanıyoruz.
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]//Tarih bilgisinin default olarak bir görüntülenme formatı var bunu istediğimiz gibi değiştirmek için kullanırız.
        public DateTime BaslamaTarihi { get; set; }
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();//bir öğretmen birden fazla kursa eğitmen olarak atanabilir. Ama bir kursun yalnız bir öğretmeni olacak 1 e çok ilişki.
    }
}