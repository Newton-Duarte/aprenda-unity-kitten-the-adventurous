public static class GlobalVariables
{
    public static int nextStage = 0;

    public static int getNextStageDescriptionIndex()
    {
        switch (nextStage)
        {
            case 3:
                return 2;
            case 4:
                return 0;
            case 5:
                return 3;
            case 6:
                return 1;
            default:
                return 1;
        }
    }
}
