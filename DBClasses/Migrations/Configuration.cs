namespace DBClasses.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DBClasses.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DBClasses.Context context)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Kvartira
            Location location_1 = new Location() {Id = 1, Name = "Your house",
                Description = "You woke up in your Moscow flat because of phone call. You answer the call and then your friend told you that somewhere nearby of Ekaterinburg they have found underground tunnels that lead to ancient artifacts with traps. What do you say?",
                Answers = new List<Answer>()};
            Answer answer_1 = new Answer() { Id = 1, Description= "Well, I'm in", PostDescrption="4" };
            Answer answer_2 = new Answer() { Id = 2, Description = "Tell some more info", PostDescrption = "2" };
            Location location_2 = new Location() { Id=2, Name = "Talk after description", Description= "It seems like it is ancient aryan and the place looks really interesting, I think you will like it.", Answers = new List<Answer>() };
            Answer answer_3 = new Answer() { Id = 3, Description = "Okay, stay home", PostDescrption = "3" }; //endgame
            location_2.Ints = "1,3";
            location_2.Answers.Add(answer_1); location_2.Answers.Add(answer_3);

            Location location_3 = new Location() { Id = 3, Name = "No, thanks", Description = "You stayed home and came to nowhere.", Answers = new List<Answer>()  };
            Answer answer_30 = new Answer() { Id = 30, Description = "EndGame", PostDescrption = "22" };
            location_3.Answers.Add(answer_30);
            location_3.Ints = "30";


            location_1.Answers.Add(answer_1); location_1.Answers.Add(answer_2); location_1.Answers.Add(answer_3);
            location_1.Ints = "1,2,3";
            //Vokzal
            Location location_4 = new Location() {Id = 4, Name = "Moscow railway station", Description = "You went to the railstation. Wou need to buy a ticket. Where do you want to go?", Answers = new List<Answer>()  };
            Answer answer_4 = new Answer() { Id = 4, Description = "To Saint-Petersburg", PostDescrption="5" };
            Answer answer_5 = new Answer() { Id = 5, Description = "To Ekaterinburg", PostDescrption = "6" };
            Answer answer_6 = new Answer() { Id = 6, Description = "To Kaliningrad", PostDescrption = "5" };
            location_4.Ints = "4,5,6";
            location_4.Answers.Add(answer_4); location_4.Answers.Add(answer_5); location_4.Answers.Add(answer_6);
            Location location_5 = new Location() {Id = 5, Name = "Wrong destination", Description = "You've chosen the wrong destination and lost everything", Answers = new List<Answer>()  }; //endgame
            location_5.Ints = "30";
            location_5.Answers.Add(answer_30);
            //Poezd
            Location location_6 = new Location() {Id = 6, Name = "Riding a train", Description = "It is really boring to travel. Russia is big and it's really long to go to other city... Conductor offers you drinks to enjoy the travel.",Answers = new List<Answer>()  };
            Answer answer_7 = new Answer() { Id = 7, Description = "Tea", PostDescrption = "7" };
            Answer answer_8 = new Answer() { Id = 8, Description = "Coffee", PostDescrption = "7" };
            Answer answer_9 = new Answer() { Id = 9, Description = "Nothing, thanks", PostDescrption = "7" };
            location_6.Ints = "7,8,9";
            location_6.Answers.Add(answer_7); location_6.Answers.Add(answer_8); location_6.Answers.Add(answer_9);

            Location location_7 = new Location() { Id = 7, Name = "Riding a train one more time", Description = "You still ride a train..", Answers = new List<Answer>()  };
            Answer answer_10 = new Answer() { Id = 10, Description = "Tea", PostDescrption = "8" };
            Answer answer_11 = new Answer() { Id = 11, Description = "Coffee", PostDescrption = "8" };
            Answer answer_12 = new Answer() { Id = 12, Description = "Nothing, thanks", PostDescrption = "8" };
            location_7.Ints = "10,11,12";
            location_7.Answers.Add(answer_10); location_7.Answers.Add(answer_11); location_7.Answers.Add(answer_12);

            //Station
            Location location_8 = new Location() { Id = 8, Name = "Ooops..", Description = "Due to endless pitching and all that drinks you felt ill. You haven't enough money to buy medicine in the train, so you went out to the station pharmacy when train stopped. There was too much people there so train came to next station without you. What should you do?", Answers = new List<Answer>()  };
            Answer answer_13 = new Answer() { Id = 13, Description = "Talk to station worker", PostDescrption = "9" };
            Answer answer_14 = new Answer() { Id = 14, Description = "Go to cafe and talk to the barkeeper", PostDescrption = "10" };
            Answer answer_15 = new Answer() { Id = 15, Description = "Walk aroud and wait", PostDescrption = "11" };
            location_8.Answers.Add(answer_13); location_8.Answers.Add(answer_14); location_8.Answers.Add(answer_15);
            location_8.Ints = "13,14,15";

            Location location_11 = new Location() {Id = 11, Name = "Wait", Description = "Nothing happend", Answers = new List<Answer>()  };
            location_11.Ints = "13,14";
            location_11.Answers.Add(answer_13); location_11.Answers.Add(answer_14);

            Location location_9 = new Location() {Id = 9, Name = "Talk to station worker", Description = "Where do you need to go? Well, understood. It's close to there. You need to take a bus numbered 410, it departs from bus station. But you know, ticket costs about 300RUB. Do you have such amount of money?", Answers = new List<Answer>()  };
            Answer answer_16 = new Answer() { Id = 16, Description = "No, I haven't :(", PostDescrption = "12" };
            location_9.Ints = "16";
            location_9.Answers.Add(answer_16);

            Location location_12 = new Location() { Id = 12, Name = "Talk to station worker continues", Description = "You can earn it here. We got a vagon full of onion and it is need to be unloaded. Do you agree?", Answers = new List<Answer>()  };
            Answer answer_17 = new Answer() { Id = 17, Description = "Yes, why not", PostDescrption = "13" };
            Answer answer_18 = new Answer() { Id = 18, Description = "No, thanks", PostDescrption = "14" };
            location_12.Ints = "17,18";
            location_12.Answers.Add(answer_17); location_12.Answers.Add(answer_18);

            Location location_14 = new Location() { Id = 14, Name = "Reject", Description = "Ok, that's your choice", Answers = new List<Answer>()  };
            Answer answer_19 = new Answer() { Id = 19, Description = "Go back to station", PostDescrption = "15" };
            location_14.Ints = "19";
            location_14.Answers.Add(answer_19);

            Location location_15 = new Location() { Id = 15, Name = "Station", Description = "You are on the station again", Answers = new List<Answer>()  };
            location_15.Ints = "14";
            location_15.Answers.Add(answer_14);

            Location location_13 = new Location() { Id = 13, Name = "Job", Description = "You did the unload and became tired. You stink, but now you have enough money!" , Answers = new List<Answer>()  };
            Answer answer_20 = new Answer() { Id = 20, Description = "Go to bus" , PostDescrption = "16"};
            location_13.Ints = "14,20";
            location_13.Answers.Add(answer_20); location_13.Answers.Add(answer_14);

            Location location_10 = new Location() { Id = 10,Name="Bar", Description = "You went to the bar. The barkeeper friendly askes you: about your motivation of going there.", Answers = new List<Answer>()  };
            Answer answer_21 = new Answer() { Id = 21, Description = "I need to go to bus station, right now! Good bye.", PostDescrption="15" };
            Answer answer_22 = new Answer() { Id = 22, Description = "Ask for hitchhiking", PostDescrption = "17"};
            location_10.Ints = "21,22";
            location_10.Answers.Add(answer_21); location_10.Answers.Add(answer_22);


            //Travel
            Location location_16 = new Location() { Id = 16, Name = "Travel by bus", Description = "So, you are on the road again. Bad music, smoky air and only and driver inside the bus.", Answers = new List<Answer>()  };
            Answer answer_23 = new Answer() { Id = 23, Description = "Listen to driver's music", PostDescrption = "17" };
            Answer answer_24 = new Answer() { Id = 24, Description = "Try to talk to the driver", PostDescrption = "18" };
            Answer answer_25 = new Answer() { Id = 25, Description = "Look throug the window", PostDescrption = "19" };
            location_16.Ints = "23,24,25";
            location_16.Answers.Add(answer_23); location_16.Answers.Add(answer_24); location_16.Answers.Add(answer_25);

            Location location_17 = new Location() { Id = 17, Name = "Russian pop-music", Description = "You are shocked by the fact that you know most of the song's lyrics", Answers = new List<Answer>()  };
            Answer answer_26 = new Answer() { Id = 26, Description = "Continue listening", PostDescrption = "20" };
            location_17.Ints = "26";
            location_17.Answers.Add(answer_26);

            Location location_18 = new Location() { Id = 19, Name = "Driver", Description = "Wanna toothpick? No? Allright.", Answers = new List<Answer>()  };
            Answer answer_27 = new Answer() { Id = 27, Description = "Listen to music", PostDescrption = "20" };
            location_18.Ints = "27";
            location_18.Answers.Add(answer_27);

            Location location_19 = new Location() { Id = 19, Name = "Nice view", Description = "Nice view. Trees, trees and some fields.", Answers = new List<Answer>()  };
            Answer answer_28 = new Answer() { Id = 28, Description = "Listen to some russian music through driver's radio", PostDescrption = "20" };
            location_19.Ints = "28";
            location_19.Answers.Add(answer_28);

            Location location_20 = new Location() { Id = 20, Name = "Sleep again", Description = "Music changes to a relaxing one", Answers = new List<Answer>()  };
            Answer answer_29 = new Answer() { Id = 29, Description = "Fall asleep", PostDescrption = "21" };
            location_20.Ints = "29";
            location_20.Answers.Add(answer_29);

            //Final location
            Location location_21 = new Location() { Id = 21, Name = "Temple", Description = "So, you are there. The temple is huge and there are lot's of your friends and colleagues. One of them greets you and escort you to the excavations. You finally arrived" ,Answers = new List<Answer>()  };
            location_21.Ints = "30";
            location_21.Answers.Add(answer_30);

            //End of the game
            Location location_end = new Location() { Id = 22, Name = "EndGame", Description = "You want to retstart?", Answers = new List<Answer>()  };
            Answer answer_31 = new Answer() { Id = 31, Description = "Yes", PostDescrption = "1" };
            location_end.Ints = "31";
            location_end.Answers.Add(answer_31);

            /*
            all_locations.Add(new Location() { 
            all_locations.Add(new Location() { Name = "Предложение работы", Description = "Можешь заработать. Нам привезли несколько вагонов с луком, их надо бы разгрузить. Сможешь?", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Работа с первым вагоном", Description = "Вы разгружаете. Очень устали. Денег все равно не хватает", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Работа со вторым вагоном", Description = "Вы разгружаете. От Вас очень пахнет луком. Денег вроде хватает", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Отказ от работы", Description = "Как хочешь", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Вокзал после работы", Description = "Наконец, уставший, но довольный Вы вновь на вокзале", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Вокзал после ожидания", Description = "Этот вокзал Вам уже прилично наскучил", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Вокзал после ожидания и работы", Description = "Жажда приключений гонит Вас вперед", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Автобус", Description = "Через некоторое время Вы снова в дороге. Дорога долгая и скучная.", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Сон в  автобусе", Description = "От накопившейся усталости Вы заснули. Удивительно, но даже тряска и русская поп-музыка не помешали Вам этого сделать.", AnswersFromLocation = new List<Answer>() });
            all_locations.Add(new Location() { Name = "Храм", Description = "Удивительно, но перед Вами место назначения: те самые раскопки. Вас встречает старый друг и проводит до недавно обнаруженных подземелий.", AnswersFromLocation = new List<Answer>() });
            */
            List<Location> all_locations = new List<Location>();
            all_locations.Add(location_1); all_locations.Add(location_2); all_locations.Add(location_3);
            all_locations.Add(location_4); all_locations.Add(location_5); all_locations.Add(location_6);
            all_locations.Add(location_7); all_locations.Add(location_8); all_locations.Add(location_9);
            all_locations.Add(location_10); all_locations.Add(location_11); all_locations.Add(location_12);
            all_locations.Add(location_13); all_locations.Add(location_14); all_locations.Add(location_15);
            all_locations.Add(location_16); all_locations.Add(location_17); all_locations.Add(location_18);
            all_locations.Add(location_19); all_locations.Add(location_20); all_locations.Add(location_21);
            all_locations.Add(location_end);
            List<Answer> all_answers = new List<Answer>();
            all_answers.Add(answer_1); all_answers.Add(answer_2); all_answers.Add(answer_3);
            all_answers.Add(answer_4); all_answers.Add(answer_5); all_answers.Add(answer_6);
            all_answers.Add(answer_7); all_answers.Add(answer_8); all_answers.Add(answer_9);
            all_answers.Add(answer_10); all_answers.Add(answer_11); all_answers.Add(answer_12);
            all_answers.Add(answer_13); all_answers.Add(answer_14); all_answers.Add(answer_15);
            all_answers.Add(answer_16); all_answers.Add(answer_17); all_answers.Add(answer_18);
            all_answers.Add(answer_19); all_answers.Add(answer_20); all_answers.Add(answer_21);
            all_answers.Add(answer_22); all_answers.Add(answer_23); all_answers.Add(answer_24);
            all_answers.Add(answer_25); all_answers.Add(answer_26); all_answers.Add(answer_27);
            all_answers.Add(answer_28); all_answers.Add(answer_29);
            all_answers.Add(answer_30); all_answers.Add(answer_31);
            
            foreach (Answer answ in all_answers)
            {
                if (!context.Answers.ToList().Exists(answe => answe.Id == answ.Id))
                {
                    context.Answers.AddOrUpdate(a => a.Id, answ);
                    context.SaveChanges();
                }
            }
            foreach(Location loc in all_locations)
            {
                if (!context.Location.ToList().Exists(locat => locat.Id == loc.Id))
                {
                    loc.Games = new List<Game>();
                    context.Location.AddOrUpdate(a => a.Id, loc);
                    context.SaveChanges();
                }
            }
        }
    }
}
