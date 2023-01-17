using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BllHeroCompany;
using DalHeroCompany.Models;
using HeroCompany.Data;
using log4net;

namespace HeroCompany.Controllers
{
    public class HeroesController : ApiController
    {
      
      

       
        [Route("api/HeroesApi/heroes")]
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<Hero> list = BllCompany.GetAllHeroes();
                

                return Ok(list);
            }
            catch (Exception)
            {
                return NotFound();

            }
            
        }

        [ResponseType(typeof(Hero))]
        [HttpGet]
        [Route("api/HeroesApi/heroes/{id}")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                Hero hero = await BllCompany.GetSpecificHero(id);
                if (hero == null)
                {
                    return NotFound();
                }
                
                return Ok(hero);
            }
            catch (Exception )
            {
            return BadRequest($"Failed to retrive hero with id {id}");
                
            }
        }


        [HttpGet]
        [Route("api/HeroesApi/heroes/Trainer/{id}")]
        public async Task<IHttpActionResult> GetHeroByTrainer(Guid id)
        {
            try
            {
                IEnumerable<Hero> heroes = await BllCompany.GetHeroesByTrainer(id);

                if (heroes == null)
                {
                    return NotFound();
                }
                
                return Ok(heroes);
            }
            catch (Exception)
            {
                
            return BadRequest($"Failed to retrive heroes to trainer with id {id}");
            }
        }


        //Train hero
        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("api/HeroesApi/heroes/{id}/trainer/{trainerId}")]
        public async Task<IHttpActionResult> Put(Guid id, Guid trainerId)
        {
            try
            {
                Hero hero = await BllCompany.TrainHero(id, trainerId);
                if (hero == null)
                {
                    return BadRequest("Hero Can't train-Check your fields...");
                }
                
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException )
            {
            return BadRequest($"Failed to Train Hero with id {id}");
                
            }

        }

        [HttpPost]
        [Route("api/HeroesApi/heroes/addHero")]
        [ResponseType(typeof(Hero))]
        public async Task<IHttpActionResult> Post(Hero hero)
        {

            bool isHeroAdded = false;
            try
            {
                isHeroAdded = await BllCompany.AddHero(hero);
                if (isHeroAdded)
                {
                    
                return Ok(hero);
                }
            }
            catch (DbUpdateException)
            {

              
            return BadRequest("Failed to Add Hero!");

            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/HeroesApi/heroes/deleteHero/{id}")]
        [ResponseType(typeof(Hero))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            bool isHeroDeleted = false;
            try
            {

                isHeroDeleted = await BllCompany.DeleteHero(id);

                if (!isHeroDeleted)
                {
                    return NotFound();
                }

                
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateException ex)
            {

            return BadRequest($"Failed to delete Hero with id {id}");
               
            }
        }

     
    }
}