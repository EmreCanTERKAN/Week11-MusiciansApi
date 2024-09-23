using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Week11_MusiciansApi.Models;

namespace Week11_MusiciansApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {

        private static List<Musicians> _musicians = new List<Musicians>()
    {
        new Musicians { Id = 1, Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar", FunnyProperties = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
        new Musicians { Id = 2, Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı", FunnyProperties = "Şarkıları yanlış anlaşılır ama çok popüler" },
        new Musicians { Id = 3, Name = "Cemil Akor", Job = "Çılgın Akorist", FunnyProperties = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
        new Musicians { Id = 4, Name = "Fatma Nota", Job = "Sürpriz Nota Üreticisi", FunnyProperties = "Nota üretirken sürekli sürprizler hazırlar" },
        new Musicians { Id = 5, Name = "Hasan Ritim", Job = "Ritim Canavarı", FunnyProperties = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
        new Musicians { Id = 6, Name = "Elif Armoni", Job = "Armoni Ustası", FunnyProperties = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
        new Musicians { Id = 7, Name = "Ali Perde", Job = "Perde Uygulayıcı", FunnyProperties = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
        new Musicians { Id = 8, Name = "Ayşe Rezonans", Job = "Rezonans Uzmanı", FunnyProperties = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
        new Musicians { Id = 9, Name = "Murat Ton", Job = "Tonlama Meraklısı", FunnyProperties = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
        new Musicians { Id = 10, Name = "Selin Akor", Job = "Akor Sihirbazı", FunnyProperties = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
    };

        //1.Get metodu Bütün listeyi döndürüyor
        [HttpGet]
        public IEnumerable<Musicians> GetAll()
        {
            return _musicians;
        }

        // FromQuery kullanımı
        [HttpGet("search")]
        public ActionResult<IEnumerable<Musicians>> GetById([FromQuery] int id)
        {
            var musician = _musicians.Where(x => x.Id == id);
            return Ok(musician);
        }

        [HttpPost]
        public ActionResult<Musicians> Create([FromBody] Musicians musician)
        {
            musician.Id = _musicians.Max(x => x.Id) + 1;
            _musicians.Add(musician);
            return CreatedAtAction(nameof(GetById), new { id = musician.Id }, musician);
        }

        // idsine göre delete işlemi yapar...
        [HttpDelete("{id:int:min(1)}")]
        public IActionResult Delete(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            _musicians.Remove(musician);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] Musicians updateMusician)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician is null)
            {
                return NotFound();
            }
            musician.Name = updateMusician.Name;
            musician.Job = updateMusician.Job;
            musician.FunnyProperties = updateMusician.FunnyProperties;
            return NoContent();
        }


        [HttpPatch("patch/{id:int:min(1)}")]
        public IActionResult PatchMusician(int id, [FromBody] JsonPatchDocument<Musicians> patchDocument)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(musician);

            return NoContent();
        }




    }
}
