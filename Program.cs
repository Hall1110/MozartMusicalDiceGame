using System.Media;


namespace MozartMusicalDiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProgramRunning();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")] // For at fjerne ligegyldige "soft errors"

        static void ProgramRunning()
        {
            bool isProgramRunning = true;
            int i, s;
            int menuetLength = 16;
            int trioLength = 16;
            string[] menuetPathArray = new string[menuetLength];
            string[] menuetFileArray = new string[menuetLength];
            string[] trioPathArray = new string[trioLength];
            string[] trioFileArray = new string[trioLength];

            string currentDirectory = Directory.GetCurrentDirectory();
            Dice dice = new Dice();
            SoundPlayer player = new SoundPlayer();



            while (isProgramRunning)
            {
                bool validInstrument = false;
                string? instrumentPlaying = "";


                Console.WriteLine("Welcome to Musikalisches Würfelspiel.");
                Console.WriteLine("-------------------------------------\n");
                Console.WriteLine("Please choose an instrument to be played.");
                Console.WriteLine("Press p for Piano");
                Console.WriteLine("Press m for Mbira");
                Console.WriteLine("Press f for Flute-harp");
                Console.WriteLine("Press c for Clarinet\n");

                while (!validInstrument)
                {
                    instrumentPlaying = Console.ReadLine();

                    switch (instrumentPlaying)
                    {
                        case "p":
                            instrumentPlaying = "piano";
                            validInstrument = true;
                            break;

                        case "m":
                            instrumentPlaying = "mbira";
                            validInstrument = true;
                            break;

                        case "f":
                            instrumentPlaying = "flute-harp";
                            validInstrument = true;
                            break;

                        case "c":
                            instrumentPlaying = "clarinet";
                            validInstrument = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice, please enter an instrument on the list.");
                            break;
                    }
                    
                }
                Console.WriteLine("\nPlease wait while I compose a viennese waltz for you.");




                string path = Path.Combine(currentDirectory, "Data", instrumentPlaying);
                for (i = 0; i < menuetLength; i++)
                {

                    int firstRoll = dice.Roll();
                    int secondRoll = dice.Roll();
                    s = firstRoll + secondRoll;
                    string instrumentPath = $"minuet{i}-{s}.wav";
                    menuetPathArray[i] = Path.Combine(path, instrumentPath);
                    menuetFileArray[i] = instrumentPath;

                }
                for (i = 0; i < trioLength; i++)
                {

                    int firstRoll = dice.Roll();
                    s = firstRoll;
                    string instrumentPath = $"trio{i}-{s}.wav";
                    trioPathArray[i] = Path.Combine(path, instrumentPath);
                    trioFileArray[i] = instrumentPath;
                }
                
                
                int foreachCounter = 0;
                foreach (string sound in menuetPathArray)
                {
                    player.SoundLocation = sound;
                    player.Load();
                    player.PlaySync();
                    ClearLine();
                    Console.Write($"\rNow playing {menuetFileArray[foreachCounter]}");
                    foreachCounter++;
                    
                }
                
                foreachCounter = 0;

                foreach (string sound in trioPathArray)
                {
                    player.SoundLocation = sound;
                    player.Load();
                    player.PlaySync();
                    ClearLine();
                    Console.Write($"\rNow playing {trioFileArray[foreachCounter]}");
                    foreachCounter++;
                    
                }



                Console.WriteLine("\n\nTo end program press x, or press any key to play again.");
                if (Console.ReadLine() == "x")
                {
                    isProgramRunning = false;
                }
            }
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}