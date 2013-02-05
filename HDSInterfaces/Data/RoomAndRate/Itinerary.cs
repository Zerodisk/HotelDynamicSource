using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDSInterfaces
{
    public class Itinerary : BaseItinerary 
    {

        public HDSSmokingPreference SmokingPreference { get; set; }

        private void init()
        {
            SmokingPreference = HDSSmokingPreference.NoPreference;

            if (Guests == null)
            {
                Guests = new List<Guest>();
            }
        }

        public Itinerary()
        {
            this.init();
        }

        public Itinerary(int numAdult, string childrenPhase)
        {
            this.init();

            this.SetGuest(numAdult, childrenPhase);
        }

        public Itinerary(int numAdult)
        {
            this.init();

            this.SetGuest(numAdult, null);
        }

        public int GetNumberOfAdult()
        {
            return ReturnNumberOfGuestByType(HDSGuestType.Adult); 
        }

        public int GetNumberOfChildren()
        {
            return ReturnNumberOfGuestByType(HDSGuestType.Child); 
        }

        /*
         * return all children age in array of integer
         */ 
        public int[] GetChildAges()
        {
            int index = 0;
            int[] result = new int[GetNumberOfChildren()];
            foreach (Guest guest in Guests)
            {
                if (guest.GuestType == HDSGuestType.Child)
                {
                    result[index] = (int)guest.Age;
                    index = index + 1;
                }
            }
            return result;
        }









        /*
         * function return number of guest type
         */ 
        private int ReturnNumberOfGuestByType(HDSGuestType guestType)
        {
            int result = 0;
            foreach (Guest guest in Guests)
            {
                if (guest.GuestType == guestType)
                {
                    result = result + 1;
                }
            }
            return result;
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
            if (string.IsNullOrEmpty(childrenPhase)) { childrenPhase = "0"; }

            for (int i = 0; i <= numAdult-1; i++)
            {
                Guest guest = new Guest();
                guest.GuestType = HDSGuestType.Adult;
                Guests.Add(guest);
            }

            string[] phase = childrenPhase.Split('_');
            int numChildren = int.Parse(phase[0]);
            
            for (int i = 0; i <= numChildren-1; i++)
            {
                Guest guest = new Guest();
                guest.GuestType = HDSGuestType.Child;
                guest.Age = int.Parse(phase[i + 1]);
                Guests.Add(guest);
            }
        }


    }
}
