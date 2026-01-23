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

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData) {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            // Use a map (Dictionary) to accumulate total points for each player
            // If the player is already in the dictionary, add to their existing total
            // If not, add them with their first point total
            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        // Convert the dictionary to an array of key-value pairs and sort by points (descending)
        var topPlayers = players.ToArray();
        Array.Sort(topPlayers, (p1, p2) => p2.Value - p1.Value);

        // Display the top 10 players with the highest point total
        Console.WriteLine();
        for (var i = 0; i < 10; ++i)
        {
            Console.WriteLine(topPlayers[i]);
        }
        
        // Results from running the program:
        // The top 10 players with the highest career points are:
        // [abdulka01, 38387] - Abdul Jabbar
        // [malonka01, 36928] - Karl Malone
        // [jordami01, 32292] - Michael Jordan
        // [chambwi01, 31419] - Wilt Chamberlain
        // [ervinju01, 30026] - Julius Erving
        // [malonmo01, 29580] - Moses Malone
        // [bryanko01, 29484] - Kobe Bryant
        // [onealsh01, 28596] - Shaquille O'Neal
        // [isselda01, 27482] - Dan Issel
        // [hayesel01, 27313] - Elgin Baylor
        // The Dictionary efficiently aggregated all seasons for each player into a single total point value,
        // then sorting by total points (descending) allowed us to identify the all-time leading scorers.
        // The Map/Dictionary is perfect for this task because it lets us:
        // 1. Quickly look up if a player exists (O(1) average lookup time)
        // 2. Update their total points as we process each row
        // 3. Store all unique player totals in one efficient data structure
    }
}