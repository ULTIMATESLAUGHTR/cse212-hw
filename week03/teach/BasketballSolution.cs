/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class BasketballSolution
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        var topPlayers = players.ToArray();
        Array.Sort(topPlayers, (p1, p2) => p2.Value - p1.Value);

        Console.WriteLine();
        for (var i = 0; i < 10; ++i)
        {
            Console.WriteLine(topPlayers[i]);
        }
    }
}

//Copying the results from running the solution which is to create a Dictionary to accumulate points per player:
// The top 10 players with the highest career points are:

//1. abdulka01 (Abdul Jabbar) - 38,387 points
//2. malonka01 (Karl Malone) - 36,928 points
//3. jordami01 (Michael Jordan) - 32,292 points
//4. chambwi01 (Wilt Chamberlain) - 31,419 points
//5. ervinju01 (Julius Erving) - 30,026 points
//6. malonmo01 (Moses Malone) - 29,580 points
//7. bryanko01 (Kobe Bryant) - 29,484 points
//8. onealsh01 (Shaquille O'Neal) - 28,596 points
//9. isselda01 (Dan Issel) - 27,482 points
//10. hayesel01 (Elgin Baylor) - 27,313 points