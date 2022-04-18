using GestBieresApi.Models;
using GestBieresApi_DAL;
using GestBieresApi_DAL.Entities;
using GestBieresApi_DAL.Services;
using ZToolbox;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace GestBieresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiereController : ControllerBase
    {
        private readonly BiereService _biereService;
        private readonly ILogger _logger;

        public BiereController(ILogger<BiereController> logger, BiereService biereService)
        {
            _biereService = biereService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get All beer is call...");
            return Ok(_biereService.Get());
        }

        [Authorize("auth")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Biere? biere = _biereService.Get(id);
            if (biere is not null)
                return Ok(biere);

            return NotFound();
        }


        [Authorize("userPolicy")]
        [HttpPost]
        public IActionResult Post(PostBiereForm form)
        {
            Biere biere = new Biere() { Nom = form.Nom, Degre = form.Degre, Origine = form.Origine};
            biere.Id = _biereService.Insert(biere);
            return Ok(biere);
        }


        [Authorize("adminPolicy")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, PutBiereForm form)
        {
            if (id != form.Id)
                return BadRequest();

            if (_biereService.Update(new Biere() { Id = form.Id, Nom = form.Nom, Degre = form.Degre, Origine = form.Origine }))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Biere? biere = _biereService.Delete(id);
            if (biere is null)
            {
                return NotFound();
            }

            return Ok(biere);
        }
    }
}
