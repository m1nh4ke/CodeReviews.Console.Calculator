namespace Menu
{
    public class Menu
    {
        public static int ShowMenu()
        {
            Console.WriteLine("Type a number, then press Enter to choose: ");
            Console.WriteLine("\t1. New calculation");
            Console.WriteLine("\t2. Show history.");
            Console.WriteLine("\t3. Delete history.");

            string? menuInput = Console.ReadLine();
            int menuChoice = 0;
            while(!int.TryParse(menuInput, out menuChoice) || menuChoice < 1 || menuChoice > 3)
            {
                Console.Write("This is not a valid input. Please enter a number from 1 to 3: ");
                menuInput = Console.ReadLine();
            }
            return menuChoice;
        }
    }
}
