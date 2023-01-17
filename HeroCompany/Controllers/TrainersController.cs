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
    public class TrainersController : ApiController
    {
        



        [HttpGet]
        [Route("api/HeroesApi/trainers")]
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<Trainer> list = BllCompany.GetAllTrainers();
               

                return Ok(list);
            }
            catch (Exception ex)
            {

            return NotFound();
               
            }
        }

        //get specific trainer data

        [HttpGet]
        [Route("api/HeroesApi/trainers/{id}")]
        [ResponseType(typeof(Trainer))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            try
            {
                Trainer trainer = await BllCompany.GetSpecificTrainer(id);
                if (trainer == null)
                {
                    return NotFound();
                }
               
                return Ok(trainer);
            }
            catch (Exception ex)
            {
               
            return BadRequest($"Failed to retrive Trainer with id {id}");
            }
        }

        [HttpPut]
        [Route("api/HeroesApi/trainers/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(Guid id, Trainer trainer)
        {
            bool isUpdatedTrainer = false;
            try
            {
                isUpdatedTrainer = await BllCompany.UpdateTrainerById(id, trainer);
                if (!isUpdatedTrainer)
                {
                    return NotFound();
                }

               
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
            return BadRequest("Failed to update trainer");
                
            }

        }

        [HttpPost]
        [Route("api/HeroesApi/trainers/SignUpTrainer")]
        [AllowAnonymous]
        [ResponseType(typeof(Trainer))]
        public async Task<IHttpActionResult> Post(Trainer trainer)
        {
            try
            {
                Trainer trainerToAdd = await BllCompany.SignUpTrainer(trainer);
                if (trainerToAdd != null)
                {
                    
                    return Ok(trainer);
                }
            }
            catch (DbUpdateException)
            {

               
            return BadRequest("Failed to add trainer");

            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/HeroesApi/trainers/{id}")]
        [ResponseType(typeof(Trainer))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            bool isTrainerDeleted = false;
            try
            {

                isTrainerDeleted = await BllCompany.DeleteTrainer(id);

                if (!isTrainerDeleted)
                {
                    return NotFound();
                }

                
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateException)
            {

            return BadRequest($"Failed to delete Trainer with id {id}");
               
            }
        }


    }
}