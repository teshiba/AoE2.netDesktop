namespace LibAoE2net
{
    using AoE2NetDesktop;

    /// <summary>
    /// Extention of Player class.
    /// </summary>
    public static class PlayerExt
    {
        /// <summary>
        /// Check Diplomacy.
        /// </summary>
        /// <param name="player1">a player.</param>
        /// <param name="player2">other player.</param>
        /// <returns>Diplomacy status.</returns>
        public static Diplomacy CheckDiplomacy(this Player player1, Player player2)
        {
            var ret = Diplomacy.Neutral;

            if ((player1.Color != null) && (player2.Color != null)) {
                if (((int)player1.Color % 2) == ((int)player2.Color % 2)) {
                    ret = Diplomacy.Ally;
                } else {
                    ret = Diplomacy.Enemy;
                }
            }

            return ret;
        }
    }
}
