namespace HeroCompany.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DalHeroCompany;
    using BCrypt;
    using BCrypt.Net;
    using DalHeroCompany.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HeroCompany.Data.HeroCompanyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HeroCompany.Data.HeroCompanyContext context)
        {
            Guid[] guid = new Guid[3] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), };
            
            context.Trainers.AddOrUpdate(x => x.Id,
        new Trainer()
        {
            Id = guid[0],
            Email = "Jack@gmail.com",
            Password = BCrypt.HashPassword("123456")
                       ,
            FullName = "Jack Reacher",
            CreatedDate = new DateTime(2022, 09, 05)
        },
        new Trainer()
        {
            Id = guid[1],
            Email = "John@gmail.com",
            Password = BCrypt.HashPassword("123456789")
                       ,
            FullName = "John Cena",
            CreatedDate = new DateTime(2022, 11, 10)
        },
            new Trainer()
            {
                Id = guid[2],
                Email = "MoAli@gmail.com",
                Password = BCrypt.HashPassword("123456"),
                FullName = "Mohammed Ali",
                CreatedDate = new DateTime(2022, 11, 15)
            }
         );

            context.Heroes.AddOrUpdate(x => x.Id,
                new Hero()
                {
                    Name ="Jojo" ,
                    Ability = "attacker",
                    Id = Guid.NewGuid(),
                    TrainBeginning = new DateTime(2022,10,10),
                    SuitColors = "Black and Blue",
                    StartingPower = 50,
                    CurrentPower= 60,
                    TrainerId = guid[0],
                    TrainCount= 0
                },
                 new Hero()
                 {
                     Name = "Momo",
                     Ability = "defender",
                     Id = Guid.NewGuid(),
                     TrainBeginning = new DateTime(2022, 12, 04),
                     SuitColors = "Yellow and Brown",
                     StartingPower = 40,
                     CurrentPower = 45,
                     TrainerId = guid[1],
                     TrainCount = 0
                 },
                  new Hero()
                  {
                      Name = "Lulu",
                      Ability = "attacker",
                      Id = Guid.NewGuid(),
                      TrainBeginning = new DateTime(2022, 12, 01),
                      SuitColors = "Red and Green",
                      StartingPower = 30,
                      CurrentPower = 34,
                      TrainerId = guid[2],
                      TrainCount = 0
                  },
                  new Hero()
                  {
                      Name = "Yaya",
                      Ability = "defender",
                      Id = Guid.NewGuid(),
                      TrainBeginning = new DateTime(2022, 10, 15),
                      SuitColors = "Black and blue",
                      StartingPower = 50,
                      CurrentPower = 60,
                      TrainerId = guid[0],
                      TrainCount = 0
                  },
                    new Hero()
                    {
                        Name = "Didi",
                        Ability = "attacker",
                        Id = Guid.NewGuid(),
                        TrainBeginning = new DateTime(2022, 11, 30),
                        SuitColors = "Pink and White",
                        StartingPower = 70,
                        CurrentPower = 80,
                        TrainerId = guid[1],
                        TrainCount = 0
                    }
                );
        }
    }
}
