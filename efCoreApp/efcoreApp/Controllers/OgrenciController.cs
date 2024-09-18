using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller 
    {    
        private readonly DataContext _context; //datacontextde ki Ogrenciler listesine Ogrenci entity türündeki öğrenci modelini ekleyeceğimiz için bir örnek alıyoruz sadece okunabilen bu nesne örneği yapıcı metotda ınişılayz edilmeli
        public OgrenciController(DataContext context)//injection yöntemi
        {
            _context = context; //private _context üyesine dışardan gelen bilgi atandı.Construckter a parametre olan bir değer doğrudan private üyeye geçilebiliyor. bu da injection yöntemidir.
        }

         public async Task<IActionResult>  Index(){

            var ogrenciler =  await _context.Ogrenciler.ToListAsync();//await ile async olduğu için beklettik.
            return View(ogrenciler);
         }


         public IActionResult Create(){
            return View();
         }

         [HttpPost]
         public async Task<IActionResult> Create(Ogrenci model){ //formdan gelecek olan entity modeli Ogrenci
            
            _context.Ogrenciler.Add(model);//_context den gelen bilgiyi Ogrenciler listesine yeni bir model olarak ekliyoruz.
            await _context.SaveChangesAsync();//async metot olduğu için await ile bekletiyoruz.
            return RedirectToAction("Index");
           
         }

         public async Task<IActionResult> Edit(int? id){
            if(id==null){ //id göndermediyse
               
               return NotFound();//404 kayıt bulunamadı hatası döner.
            }
           
             var ogr = await _context.Ogrenciler.Include(o => o.KursKayitlari).ThenInclude(o => o.Kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);//FirstOrDefaultAsync(o => o.OgrenciId == id) e postaya göre veya farklı kriterlere göre arama yapmayı sağlar fakat FindAsync sadece id için kullanılabilir.
             

             if(ogr == null){ //veritabanında ilgili ıd ile eşleşen kayıt yoksa
                
                return NotFound();
             }

               return View(ogr);//güncellenmek istenen ıd de ki öğrenci kaydı get edildi.
            

         }


         [HttpPost]
         [ValidateAntiForgeryToken]//post ve get işlemini aynı kişinin yapıp yapmadığını güvenlik açısından kontrol için.
    
         public async Task<IActionResult> Edit(int id, Ogrenci model)
     {
         if(id != model.OgrenciId)
       {
          return NotFound();
       }

       if(ModelState.IsValid)
       {
         try
         {
             _context.Update(model);
             await _context.SaveChangesAsync();
         }
         catch(DbUpdateConcurrencyException)
         {
             if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
             {
                 return NotFound();
             } 
             else 
             {
                 throw;
             }
         }
         return RedirectToAction("Index");
       }

          return View(model);
     }

        
        [HttpGet]
               
        public async Task<IActionResult> Delete(int? id) //Kullanıcıya silmek konusunda emin mi diye sormak için Ogrenci kaydını tekrar çektik.
        {
            if(id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(id);

            if(ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }
     
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)//formdan gelen id bilgisini aldığımızı ifade ettik.
       {
          var ogrenci = await _context.Ogrenciler.FindAsync(id);
          if(ogrenci == null)
        {
            return NotFound();
        }
          _context.Ogrenciler.Remove(ogrenci);
           await _context.SaveChangesAsync();
           return RedirectToAction("Index");
       }




    }
}