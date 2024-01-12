public sealed class EnemyDeath
{
    private static EnemyDeath instance;
    private int count = 1;

    public static EnemyDeath Instance
    {
        get
        {
            if (instance == null)
                instance = new EnemyDeath();
            return instance;
        }
    }

    public int Count { get => count; set => count = value; } 
}
