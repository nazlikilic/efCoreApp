using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller
    {   
        private readonly DataContext _context;//sadece okuanbilen private dbcontext nesnesini aldık 
        public KursKayitController(DataContext context)//dbcontext nesnemizi public yapıcı metotda inişılayz ettik.
        {
            _context = context;
        }
         public async Task<IActionResult>  Index()
         {  
               return View(await _context.KursKayitlari.Include(x => x.Ogrenci).Include(x => x.Kurs).ToListAsync());// hangi öğrencinin hangi kursa kayıt olduğu kurs kayıtlarını listelemek için.
         }

        
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(),"OgrenciId","AdSoyad");//select list ile ogrenci seçimi yaptırılıyor.
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(),"KursId","Baslik");//selectlist ile kurs seçimi yaptırılıyor.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(KursKayit model)
        {   
            model.KayitTarihi =  DateTime.Now;//kayıt bilgisi modelden gelmiyor ama ne zaman yapıldıysa tarihi modele ekleyip o şekilde database e göndermiş olduk.
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}