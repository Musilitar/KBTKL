public class Minigames
{
    private static Minigames instance;
    public enum AvailableMinigames
    {
        RHYTHM,
        CODE,
        DEBATE,
        BADMINTON,
        HARDWARE
    }

    public static Minigames Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Minigames();
            }
            return instance;
        }
    }
}
