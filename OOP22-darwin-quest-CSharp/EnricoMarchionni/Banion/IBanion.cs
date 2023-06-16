namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion
{
    public interface IBanion
    {
        public const uint NUM_MOVES = 4;
        public const uint MIN_HP = 0;

        public event EventHandler<IBanion> BanionChanged;

        bool IsAlive { get; }

        // Waiting for Raffaele to push IMove
        //ISet<IMove> Moves { get; }
        //bool ReplaceMove(IMove oldOne, IMove newOne);

        uint Hp { get; }

        uint MaxHp { get; }

        void IncreaseHp(uint amount);

        void DecreaseHp(uint amount);

        void SetHpToMax();

        //double Attack { get; set; }

        //void IncreaseAttack(double amount);

        //void DecreaseAttack(double amount);

        //double Defence { get; set; }

        //void IncreaseDefence(double amount);

        //void DecreaseDefence(double amount);

        //int Level { get; }

        //void IncreaseLevel();

        //int Xp { get; }

        //int MaxXp { get; }

        //void IncreaseXp(int amount);
    }
}
