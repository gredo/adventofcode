﻿using System;

public class Script12b
{
    // Read the file as array per line
    string[] lines = System.IO.File.ReadAllLines(@"C:\projects\adventofcode\data\2022\12\input.txt");

    List<string> map = new List<string>();
    Tile finish = new Tile();
    
    List<Tile> starts = new List<Tile>();

    public void run()
    {
        this.prepareData();

        int minimalPathSize = 0;

        foreach (Tile start in starts)
        {
            int resultOneTile = processOneStart(start);
            if (resultOneTile > 0 && (minimalPathSize == 0 || minimalPathSize > resultOneTile)) 
            {
                minimalPathSize = resultOneTile;
            }
        }

        Console.WriteLine($"Result: {minimalPathSize}");

    }

    private void prepareData()
    {
        foreach (string line in lines)
        {
            map.Add(line);
        }

        finish.Y = map.FindIndex(x => x.Contains("E"));
        finish.X = map[finish.Y].IndexOf("E");

        for (int i =0; i < map.Count; i++)
        {
            var line = map[i];
            for (int j = 0; j < map.Count; j++)
            {
                if (line[j] == 'a' || line[j] == 'S')
                {
                    Tile start = new Tile();
                    start.Y = i;
                    start.X = j;
                    start.SetDistance(finish.X, finish.Y);
                    starts.Add(start);
                }
            }
        }
    }

    private int processOneStart(Tile start)
    {
        List<Tile> activeTiles = new List<Tile>();
        List<Tile> visitedTiles = new List<Tile>();
        activeTiles.Add(start);

        while (activeTiles.Any())
        {
            var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

            if (checkTile.X == finish.X && checkTile.Y == finish.Y)
            {
                //Console.WriteLine("We are at the destination!");
                var tile = checkTile;
                var i = 0;
                while (true)
                {
                    i++;
                    if (map[tile.Y][tile.X] == ' ')
                    {
                        var newMapRow = map[tile.Y].ToCharArray();
                        newMapRow[tile.X] = '*';
                        map[tile.Y] = new string(newMapRow);
                    }
                    tile = tile.Parent;
                    if (tile == null)
                    {
                        //Console.WriteLine($"Fields in path: {i - 1}");
                        return i - 1;
                    }
                }
            }

            visitedTiles.Add(checkTile);
            activeTiles.Remove(checkTile);

            var walkableTiles = this.GetWalkableTiles(map, checkTile, finish);

            foreach (var walkableTile in walkableTiles)
            {
                //We have already visited this tile so we don't need to do so again!
                if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    continue;

                //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                {
                    var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                    if (existingTile.CostDistance > checkTile.CostDistance)
                    {
                        activeTiles.Remove(existingTile);
                        activeTiles.Add(walkableTile);
                    }
                }
                else
                {
                    //We've never seen this tile before so add it to the list. 
                    activeTiles.Add(walkableTile);
                }
            }
        }

        //Console.WriteLine("No Path Found for this start!");
        return -1;
    }

    private List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>()
    {
        new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
        new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
    };

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

        var maxX = map.First().Length - 1;
        var maxY = map.Count - 1;

        return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                .Where(tile =>  (map[tile.Y][tile.X] >= 'a' && map[tile.Y][tile.X] <= 'z' && map[currentTile.Y][currentTile.X] >= 'a' && map[currentTile.Y][currentTile.X] <= 'z' && map[tile.Y][tile.X] > (int)map[currentTile.Y][currentTile.X] && ((int)map[tile.Y][tile.X]-(int)map[currentTile.Y][currentTile.X]) <2)
                             || (map[tile.Y][tile.X] >= 'a' && map[tile.Y][tile.X] <= 'z' && map[currentTile.Y][currentTile.X] >= 'a' && map[currentTile.Y][currentTile.X] <= 'z' && map[tile.Y][tile.X] <= (int)map[currentTile.Y][currentTile.X])
                             || (map[tile.Y][tile.X] == 'E' && map[currentTile.Y][currentTile.X] == 'z')
                             || (map[tile.Y][tile.X] == 'E' && map[currentTile.Y][currentTile.X] == 'y')
                             || (map[currentTile.Y][currentTile.X] == 'S' && map[tile.Y][tile.X] == 'a')
                             || (map[currentTile.Y][currentTile.X] == 'S' && map[tile.Y][tile.X] == 'b')
                      )
                .ToList();
    }
}
