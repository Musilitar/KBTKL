class Stats
{
    private static Stats instance;
    public enum AvailableStats
    {
        TYPING,
        TECHNOLOGY,
        MUSICALITY,
        ARGUMENTATION,
        SPORTINESS,
        AUTISM
    }

    public static Stats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Stats();
            }
            return instance;
        }
    }
}
