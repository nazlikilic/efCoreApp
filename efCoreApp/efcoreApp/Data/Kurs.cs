namespace efcoreApp.Data
{
    public class Kurs
    { 
        public int KursId {get; set;}

        public string? Baslik {get; set;}

        public Ogretmen Ogretmen { get; set; } = null!;//her kursun yalnız bir öğretmeni olacak dolayısıyla ICollection kullanmamıza gerek yok.
        public int? OgretmenId {get; set;} //kurs açıldıktan sonra öğretmen ataması yapılacak olabilir o yüzden ?.
        
        public ICollection<KursKayit> KursKayitlari {get;set;}= new List<KursKayit>();
    }

}