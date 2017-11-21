using DBClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegrammClient
{
    public class Client
    {
        private Repository Repo { get; set; }

        public Client(Repository _repo)
        {
            Repo = _repo;
        }

        public void Show(Location location)
        {
            //send description
            //send buttons
            //add to this game's log new key - location
        }

        public void Answered(int chatId, int buttonId)
        {
            string postDescription = Repo.GetAnswer(buttonId).PostDescrption;
            //send postDescription
            int new_location_id = Repo.GetLocation(Repo.GetAnswer(buttonId).ToLocation.Id).Id;
            Repo.ChangeLocation(chatId, new_location_id);
            Show(Repo.GetLocation(new_location_id));
            //done1
        }

    }
}
