using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class KursKayit
    { 
        [Key]
        public int KayitId { get; set; }

        public int OgrenciId {get; set;}
        public  Ogrenci Ogrenci {get; set;} = null!; //bu sefer string veya int tipinde değil Ogrenci modeli tipinde bir property eklemiş oluyoruz. Bu veritabanındaki JOİN sorgusuna karşılık gelmektedir.

        public int KursId { get; set; }
        public Kurs Kurs {get; set;} = null!;//navigation property
        
        public DateTime KayitTarihi {get; set;}
        

    }
//1 numaralı kayıt oluşturuldu. 5 numaralı öğrenci 2 numaralı kursa kayıt oldu.
}