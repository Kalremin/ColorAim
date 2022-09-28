public static class RankingData
{
    static string[] rankingName= new string[] { "firstName", "secondName", "thirdName", "forthName", "fifthName" };
    static string[] rankingScore = new string[] { "firstScore", "secondScore", "thirdScore", "forthScore", "fifthScore" };

    public static string[] GetNameData => rankingName;
    public static string[] GetScoreData => rankingScore;
}

