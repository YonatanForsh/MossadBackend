namespace MossadBackend.Directions
{
    public static class Direction
    {
        public static Dictionary<string, List<int>> _direction = new Dictionary<string, List<int>>()
        {
            { "nw", [-1, 1] },
            { "n" , [0, 1] },
            { "ne" , [1, 1] },
            { "w" , [-1, 0] },
            { "e" , [1, 0] },
            { "sw" , [-1, -1] },
            { "s" , [0, -1] },
            { "se" , [1, -1] }
        };
    }
}
