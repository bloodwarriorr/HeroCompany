using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using BCrypt.Net;
using DalHeroCompany.Models;
using HeroCompany.Models;
namespace BllHeroCompany
{
    public static class BllCompany
    {
        readonly private static heroCompanyEntities db = new heroCompanyEntities();

        public static IEnumerable<Hero> GetAllHeroes()
        {
            return db.Heroes.ToList();
            
        }
        public static async Task<Hero> GetSpecificHero(Guid id)
        {
            Hero hero = await db.Heroes.FindAsync(id);
            if (hero == null)
            {
                return null;
            }
            return hero;
        }

        public static async Task<IEnumerable<Hero>> GetHeroesByTrainer(Guid id)
        {
            List<Hero> heroes = await db.Heroes.ToListAsync();
            if (heroes == null)
            {
                return null;
            }
            
            heroes = heroes.FindAll(h => h.TrainerId == id);
            return heroes;
        }

        public static async Task<Hero> TrainHero(Guid id,Guid trainerId)
        {
            try
            {
            Hero hero = await db.Heroes.FindAsync(id);
            if (!ValidateHero(hero, trainerId))
            {
                return null;
            }
            else
            {
                Random randomGrowthPercent = new Random();
                hero.TrainBeginning = DateTime.Now;
                hero.TrainCount++;
                hero.CurrentPower += (hero.CurrentPower * randomGrowthPercent.Next(1, 10)) / 100;
                db.Entry(hero).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return hero;
            }
            }
            catch (DbUpdateException ex)
            {

                throw new DbUpdateException(ex.Message, ex);
            }
        }

        public static async Task<bool> AddHero(Hero hero)
        {
            try
            {
                hero.Ability = hero.Ability != "attacker" && hero.Ability != "defender" ? "attacker" : hero.Ability;
                db.Heroes.Add(hero);
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message,ex);
            }
          
            
        }
        public static async Task<bool> DeleteHero(Guid id)
        {
            try
            {
                Hero hero = await db.Heroes.FindAsync(id);
                db.Heroes.Remove(hero);
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {

                throw new DbUpdateException(ex.Message, ex);
            }
           


        }
        //help method to validate hero training
        private static bool ValidateHero(Hero hero, Guid trainerId)
        {

            if (hero == null)
            {
                return false;
            }
            //use to reset training amount if it is another day
            else if (hero.TrainBeginning.Date != DateTime.Now.Date)
            {
                hero.TrainCount = 0;
            }
            else if (hero.TrainerId != trainerId)
            {
                return false;
            }
            //check if the hero trained more than 5 times today
            else if (hero.TrainCount >= 5 && hero.TrainBeginning.Date == DateTime.Now.Date)
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<Trainer> GetAllTrainers()
        {
            return db.Trainers.ToList();
        }
        public static async Task<Trainer> GetSpecificTrainer(Guid id)
        {
            Trainer trainer = await db.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return null;
            }
            return trainer;
        }

        public static async Task<bool> UpdateTrainerById(Guid id, Trainer trainer)
        {
            try
            {
            Trainer updateTrainer = await db.Trainers.FindAsync(id);
            if (updateTrainer==null)
            {
                return false;
            }
                UpdateTrainer(updateTrainer, trainer);
                db.Entry(trainer).State = EntityState.Modified;
                await db.Entry(trainer).ReloadAsync();
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message,ex);
            }
            
            
        }

        public static async Task<Trainer> SignUpTrainer(Trainer trainer)
        {
            try
            {
                trainer.Password = BCrypt.Net.BCrypt.HashPassword(trainer.Password);
                db.Trainers.Add(trainer);
                await db.SaveChangesAsync();
                return trainer;
            }
            catch (DbUpdateException ex)
            {
  
                throw new DbUpdateException(ex.Message, ex);
            }
           


        }

        public static async Task<bool> DeleteTrainer(Guid id)
        {
            try
            {
                Trainer trainer = await db.Trainers.FindAsync(id);
                db.Trainers.Remove(trainer);
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {

                throw new DbUpdateException(ex.Message, ex);
            }



        }
        private static void UpdateTrainer(Trainer trainerToUpdate, Trainer newTrainerValues)
        {
            if (trainerToUpdate.FullName != newTrainerValues.FullName)
            {
                trainerToUpdate.FullName = newTrainerValues.FullName;
            }
            if (trainerToUpdate.Email != newTrainerValues.Email)
            {
                trainerToUpdate.Email = newTrainerValues.Email;
            }
            if (!BCrypt.Net.BCrypt.Verify(newTrainerValues.Password, trainerToUpdate.Password))
            {
                trainerToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(newTrainerValues.Password);
            }
            if (trainerToUpdate.CreatedDate.Date != newTrainerValues.CreatedDate.Date)
            {
                trainerToUpdate.CreatedDate = newTrainerValues.CreatedDate;
            }

        }






    }
}
