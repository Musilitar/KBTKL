using System.Collections.Generic;

public class Classes {
    private static Classes instance;
    public enum AvailableClasses
    {
        CORWIN,
        DAAN,
        NIELS,
        LODE,
        VIC
    }
    public Dictionary<string, string> Corwin = new Dictionary<string, string>()
    {
        {"minigame",  Minigames.AvailableMinigames.RHYTHM.ToString()},
        {"description", "Uses the violin to slay his foes"},
        {"primaryStat", Stats.AvailableStats.MUSICALITY.ToString()},
    };

    public static Classes Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Classes();
            }
            return instance;
        }
    }
}
