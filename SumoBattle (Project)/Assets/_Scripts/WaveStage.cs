public sealed class WaveStage
{
    private static WaveStage instance;

    private int stage = 1;
    
    public static WaveStage Instance
    {
        get
        {
            if (instance == null)
                instance = new WaveStage();
            return instance;
        }
    }

    public int Stage { get => stage; set => stage = value; }
}
