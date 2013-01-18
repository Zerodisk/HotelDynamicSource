using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Itinerary : BaseItinerary 
    {

        public HDSSmokingPreference SmokingPreference { get; set; }

        public Itinerary()
        {
            SmokingPreference = HDSSmokingPreference.NoPreference;

            if (Guests == null)
            {
                Guests = new List<Guest>();
            }
        }

        public Itinerary(int numAdult, string childrenPhase)
        {
            SmokingPreference = HDSSmokingPreference.NoPreference;

            if (Guests == null){
                Guests = new List<Guest>();
            }

            this.SetGuest(numAdult, childrenPhase);
        }






        /*
         * for setting guest in the room
         * - adult: this is number of adults
         *     create number of guest object and set to type=adult
         * - children: format is NumberOfChildren_Age1_Age2_Age3
         *       ex: 2_3_6 = 2 children with age 3 and 6
         *       ex: 1_2   = 1 child with age 2
         *     create number of guest object and set to type=child with age
         *     NOTE: validation of childrenPhase must be done before
         */
        private void SetGuest(int numAdult, string childrenPhase)
        {
            for (int i = 0; i <= numAdult; i++)
            {
                Guest guest = new Guest();
                guest.GuestType = HDSGuestType.Adult;
                Guests.Add(guest);
            }

            string[] phase = childrenPhase.Split('_');
            int numChildren = int.Parse(phase[0]);
            
            for (int i = 0; i <= numChildren; i++)
            {
                Guest guest = new Guest();
                guest.GuestType = HDSGuestType.Child;
                guest.Age = int.Parse(phase[i + 1]);
                Guests.Add(guest);
            }
        }


    }
}
