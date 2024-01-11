public sealed class EnemyDeathCount
{
    private static EnemyDeathCount instance;
    private int count = 1;

    public static EnemyDeathCount Instance
    {
        get
        {
            if (instance == null)
                instance = new EnemyDeathCount();
            return instance;
        }
    }

    public int Count { get => count; set => count = value; } 
}
