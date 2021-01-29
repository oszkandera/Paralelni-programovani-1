namespace PageRank
{
    public static class PageRankFactory
    {
        public static double[] CreateEmptyPageRankArray(int size)
        {
            return new double[size];
        }

        public static double[] CreateDefaultPageRankArray(int size)
        {
            var pageRankArray = CreateEmptyPageRankArray(size);
            double defaultRank = (double)1 / size;
            for (int x = 0; x < size; x++)
            {
                pageRankArray[x] = defaultRank;
            }

            return pageRankArray;
        }
    }
}
