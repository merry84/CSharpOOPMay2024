namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            /*•	The First line consists of phone numbers: a string, separated by spaces.
             •	The Second line consists of websites: a string, separated by spaces.
             */
            string[] phoneInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] webSiteInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICalling phone;
            /*§	If the number is 10 digits long, you are making a call from your smartphone 
             * 
            §	If the number is 7 digits long, you are making a call from your stationary phone  */
            foreach (var numer in phoneInfo)
            {
                if (numer.Length == 7)
                {
                    phone = new StationaryPhone();
                }
                else
                {
                    phone = new SmartPhone();
                }
                phone.Calling(numer);
            }
            IBrowsing smartPhone = new SmartPhone();

            foreach (var site in webSiteInfo)
            {
                smartPhone.BrowsingURl(site);
            }
        }
    }
}
