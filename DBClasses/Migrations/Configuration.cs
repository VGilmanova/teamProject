namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DBClasses.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.
        protected override void Seed(DBClasses.Context context)
        {
            Location location1 = new Location() { Id = 1, Name = "Start location", Description = "You are on a glade full of flowers", AnswersToThisLocation = new  List<Answer>(), Games = new List<Game>()};
            Location location2 = new Location() { Id = 2, Name = "Forest gate", Description = "Trees are so high and huge that you can't see the sky", AnswersToThisLocation = new List<Answer>(), Games = new List<Game>() };
            Answer answer1 = new Answer() { Id = 1, Description = "Enter the forest", PostDescrption = "You walked into dark and wet forest", ToLocation = location2 };
            location1.Answer_1 = answer1;
            location2.AnswersToThisLocation.Add(answer1);
            context.Location.AddOrUpdate(a => a.Id, location1);
            context.Location.AddOrUpdate(a => a.Id, location2);
            context.SaveChanges();
            context.Answers.AddOrUpdate(a => a.Id, answer1);
            context.SaveChanges();
        }
    }
}
