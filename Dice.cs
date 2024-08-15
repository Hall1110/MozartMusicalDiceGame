namespace MozartMusicalDiceGame
{
    internal class Dice
    {
        public int Sides { get; set; }



        public Dice()
        {
            Sides = 6;
        }



        public Dice ChangeDefaultDiceSize(int sides)
        {
            Sides = sides;
            return this;
        }

        private readonly Random random = new Random();

        public int Roll()
        {

            int diceRoll = random.Next(1, Sides + 1);
            return diceRoll;
        }
    }
}
