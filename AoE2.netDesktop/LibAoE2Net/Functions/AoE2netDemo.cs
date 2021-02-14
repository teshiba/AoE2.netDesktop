namespace LibAoE2net
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Demo class of AoE2net.
    /// </summary>
    public class AoE2netDemo
    {
        /// <summary>
        /// Gets steam ID for demo.
        /// </summary>
        public const string SteamId = "demo";

        /// <summary>
        /// Gets Profil ID for demo.
        /// </summary>
        public const int ProfilId = 0;

        /// <summary>
        /// Gets endPoint of PlayerRatingHistory.
        /// </summary>
        public const string EndPointPlayerRatingHistory = "demo/PlayerRatingHistory";

        /// <summary>
        /// Gets endPoint of PlayerLastmatch.
        /// </summary>
        public const string EndPointPlayerLastmatch = "demo/PlayerLastmatch";

        /// <summary>
        /// Gets json text of PlayerRatingHistory.
        /// </summary>
        public const string JsonPlayerRatingHistory = @"[{""rating"":9999,""num_wins"":100,""num_losses"":100,""streak"":0,""drops"":0,""timestamp"":1643808142}]";

        /// <summary>
        /// Gets json text PlayerLastmatch.
        /// </summary>
        public const string JsonPlayerLastmatch = @"{""profile_id"":0000000,""steam_id"":""demo"",""name"":""Player1"",""country"":""JP"",
""last_match"":{""match_id"":""00000000"",""lobby_id"":null,""match_uuid"":""00000000-0000-0000-0000-000000000000"",""version"":""00000"",
""name"":""AUTOMATCH"",""num_players"":8,""num_slots"":8,""average_rating"":null,""cheats"":false,""full_tech_tree"":false,""ending_age"":5,
""expansion"":null,""game_type"":0,""has_custom_content"":null,""has_password"":true,""lock_speed"":true,""lock_teams"":true,""map_size"":4,
""map_type"":29,""pop"":200,""ranked"":true,""leaderboard_id"":4,""rating_type"":4,""resources"":1,""rms"":null,""scenario"":null,""server"":""demo"",
""shared_exploration"":false,""speed"":2,""starting_age"":2,""team_together"":true,""team_positions"":true,""treaty_length"":0,""turbo"":false,""victory"":1,
""victory_time"":0,""visibility"":0,""opened"":1612182081,""started"":1612182081,""finished"":1643808142,
""players"":[
{""profile_id"":0000000,""steam_id"":""demo"", ""name"":""Player1"",""clan"":null,""country"":null,""slot"":1,""slot_type"":1,""rating"":1111,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":1,""team"":1,""civ"":11,""won"":false},
{""profile_id"":0000001,""steam_id"":""demo"",""name"":""Player2"",""clan"":null,""country"":null,""slot"":2,""slot_type"":1,""rating"":2222,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":2,""team"":2,""civ"":24,""won"":true},
{""profile_id"":0000002,""steam_id"":""demo"",""name"":""Player3"",""clan"":null,""country"":null,""slot"":3,""slot_type"":1,""rating"":3333,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":3,""team"":1,""civ"":9,""won"":false},
{""profile_id"":0000003,""steam_id"":""demo"",""name"":""Player4"",""clan"":null,""country"":null,""slot"":4,""slot_type"":1,""rating"":4444,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":4,""team"":2,""civ"":11,""won"":true},
{""profile_id"":0000004,""steam_id"":""demo"",""name"":""Player5"",""clan"":null,""country"":null,""slot"":5,""slot_type"":1,""rating"":5555,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":5,""team"":1,""civ"":24,""won"":false},
{""profile_id"":0000005,""steam_id"":""demo"",""name"":""Player6"",""clan"":null,""country"":null,""slot"":6,""slot_type"":1,""rating"":6666,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":6,""team"":2,""civ"":12,""won"":true},
{""profile_id"":0000006,""steam_id"":""demo"",""name"":""Player7"",""clan"":null,""country"":null,""slot"":7,""slot_type"":1,""rating"":null,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":7,""team"":1,""civ"":35,""won"":false},
{""profile_id"":0000007,""steam_id"":""demo"",""name"":""Player8"",""clan"":null,""country"":null,""slot"":8,""slot_type"":1,""rating"":7777,""rating_change"":null,""games"":null,""wins"":null,""streak"":null,""drops"":null,""color"":8,""team"":2,""civ"":36,""won"":true}
]}}
";

        private static readonly Dictionary<string, string> JsonTextList = new Dictionary<string, string> {
            { EndPointPlayerRatingHistory,  JsonPlayerRatingHistory },
            { EndPointPlayerLastmatch,  JsonPlayerLastmatch },
        };

        /// <summary>
        /// deserializing the apiEndPoint body as JSON.
        /// </summary>
        /// <typeparam name="T">deserialize as this type.</typeparam>
        /// <param name="apiEndPoint">API end point.</param>
        /// <returns>JSON deserialize object.</returns>
        public static T GetJsonText<T>(string apiEndPoint)
            where T : new()
        {
            T ret = default;
            var result = JsonTextList.TryGetValue(apiEndPoint, out string jsonText);
            if (result) {
                var serializer = new DataContractJsonSerializer(typeof(T));
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                ret = (T)serializer.ReadObject(stream);
            }

            return ret;
        }

        /// <summary>
        /// Gets whether the demo class has the specified endpoint.
        /// </summary>
        /// <param name="endPoint">API endpoint string.</param>
        /// <returns>Whether the specified endpoint is available.</returns>
        public static bool HasEndPoint(string endPoint)
        {
            return endPoint.StartsWith("demo/");
        }
    }
}