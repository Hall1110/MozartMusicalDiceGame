using System.Media;


namespace MozartMusicalDiceGame
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
            ProgramRunning();
        }


        static void ProgramRunning()
        {
            bool isProgramRunning = true;
            int menuetLength = 16;
            int trioLength = 16;
            string[] menuetPathArray = new string[menuetLength];
            string[] menuetFileArray = new string[menuetLength];
            string[] trioPathArray = new string[trioLength];
            string[] trioFileArray = new string[trioLength];

            

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

                CreatePathArray(menuetPathArray, menuetFileArray, instrumentPlaying, "minuet");
                CreatePathArray(trioPathArray, trioFileArray, instrumentPlaying, "trio");

                PlaySoundFromPathArray(menuetPathArray, menuetFileArray);
                PlaySoundFromPathArray(trioPathArray, trioFileArray);


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



        /// <summary>
        /// Creates the paths for the sound files and saves them in the pathArray, while saving the individual file names in fileArray.
        /// Also needs the current chosen instrument, and the type (Menuet or Trio)
        /// Returns a completed pathArray and a completed fileArray.
        /// </summary>
        /// <param name="pathArray"></param>
        /// <param name="fileArray"></param>
        /// <param name="currentInstrument"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static (string[], string[]) CreatePathArray(string[] pathArray, string[] fileArray, string currentInstrument, string type)
        {
            int pathArrayLength = pathArray.Length;
            Dice dice = new Dice();
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(currentDirectory, "Data", currentInstrument);

            
            for (int i = 0; i < pathArrayLength; i++)
            {
                int s;
                if (type == "minuet")
                {
                int firstRoll = dice.Roll();
                int secondRoll = dice.Roll();
                s = firstRoll + secondRoll;
                }
                else
                {
                    s = dice.Roll();
                }
                string instrumentPath = $"{type}{i}-{s}.wav";
                pathArray[i] = Path.Combine(path, instrumentPath);
                fileArray[i] = instrumentPath;
            }
            return (pathArray, fileArray);
        }


        // For at fjerne ligegyldige "soft errors"
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")] 
        /// <summary>
        /// Takes the pathArray and fileArray as parameters.
        /// Plays the sounds files in the pathArray, while displaying the fileArray in the Console.
        /// </summary>
        public static void PlaySoundFromPathArray(string[] pathArray, string[] fileArray)
        {
            int foreachCounter = 0;
            SoundPlayer player = new SoundPlayer();

            foreach (string sound in pathArray)
            {
                player.SoundLocation = sound;
                player.Load();
                player.PlaySync();
                ClearLine();
                Console.Write($"\rNow playing {fileArray[foreachCounter]}");
                foreachCounter++;
            }
        }
    }
}