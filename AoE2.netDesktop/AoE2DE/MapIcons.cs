namespace AoE2NetDesktop.AoE2DE;

using System.Collections.Generic;

/// <summary>
/// Map Icons class.
/// </summary>
public class MapIcons
{
    private const string MapiconsPath = $@"widgetui\textures\menu\mapicons\";

#if false // Not defined!
"cm_canals.DDS"
"cm_capricious.DDS"
"cm_dingos.DDS"
"cm_generic.DDS"
"cm_graveyards.DDS"
"cm_metropolis.DDS"
"cm_moats.DDS"
"cm_paradise-island.DDS"
"cm_pilgrims.DDS"
"cm_prairie.DDS"
"cm_seasons.DDS"
"cm_sherwood-forest.DDS"
"cm_sherwood-heroes.DDS"
"cm_shipwreck.DDS"
"cm_team-glaciers.DDS"
"cm_the-unknown.DDS"
"rwm_siberia.DDS"
"sm_journey-south.DDS"
"sm_snake-forest.DDS"
"sm_sprawling-stream.DDS"
"sm_swirling-river.DDS"
"sm_twin-forests.DDS"
#endif

    private static readonly Dictionary<int, string> MapIconFielNames = new () {
        { 9, "rm_arabia" },
        { 10, "rm_archipelago" },
        { 11, "rm_baltic" },
        { 12, "rm_black-forest" },
        { 13, "rm_coastal" },
        { 14, "rm_continental" },
        { 15, "rm_crater-lake" },
        { 16, "rm_fortress" },
        { 17, "rm_gold-rush" },
        { 18, "rm_highland" },
        { 19, "rm_islands" },
        { 20, "rm_mediterranean" },
        { 21, "rm_migration" },
        { 22, "rm_rivers" },
        { 23, "rm_team-islands" },
        { 24, "rm_full-random" },
        { 25, "rm_scandinavia" },
        { 26, "rm_mongolia" },
        { 27, "rm_yucatan" },
        { 28, "rm_salt-marsh" },
        { 29, "rm_arena" },
        { 31, "rm_oasis" },
        { 32, "rm_ghost-lake" },
        { 33, "rm_nomad" },
        { 49, "rwm_iberia" },
        { 50, "rwm_britain" },
        { 51, "rwm_mideast" },
        { 52, "rwm_texas" },
        { 53, "rwm_italy" },
        { 54, "rwm_central_america" },
        { 55, "rwm_france" },
        { 56, "rwm_norse_lands" },
        { 57, "rwm_sea_of_japan" },
        { 58, "rwm_byzantium" },
        { 59, "Custom" },                       // Not defined!
        { 60, "rm_random_land_map" },
        { 62, "rwm_random_real_world_map" },
        { 63, "rm_blind_random" },
        { 65, "Random Special Map" },           // Not defined!
        { 66, "Random Special Map" },           // Not defined!
        { 67, "rm_acropolis" },
        { 68, "rm_budapest" },
        { 69, "rm_cenotes" },
        { 70, "rm_city-of-lakes" },
        { 71, "rm_golden-pit" },
        { 72, "rm_hideout" },
        { 73, "rm_hill-fort" },
        { 74, "rm_lombardia" },
        { 75, "rm_steppe" },
        { 76, "rm_valley" },
        { 77, "rm_megarandom" },
        { 78, "rm_hamburger" },
        { 79, "rm_ctr_random" },
        { 80, "rm_ctr_monsoon" },
        { 81, "rm_ctr_pyramid-descent" },
        { 82, "rm_ctr_spiral" },
        { 83, "rm_kilimanjaro" },
        { 84, "rm_mountain-pass" },
        { 85, "rm_nile-delta" },
        { 86, "rm_serengeti" },
        { 87, "rm_socotra" },
        { 88, "rwm_amazon" },
        { 89, "rwm_china" },
        { 90, "rwm_horn_of_africa" },
        { 91, "rwm_india" },
        { 92, "rwm_madagascar" },
        { 93, "rwm_west_africa" },
        { 94, "rwm_bohemia" },
        { 95, "rwm_earth" },
        { 96, "sm_canyons" },
        { 97, "sm_enemy-archipelago" },
        { 98, "sm_enemy-islands" },
        { 99, "sm_far-out" },
        { 100, "sm_front-line" },
        { 101, "sm_inner-circle" },
        { 102, "sm_motherland" },
        { 103, "sm_open-plains" },
        { 104, "sm_ring-of-water" },
        { 105, "sm_snake-pit" },
        { 106, "sm_the-eye" },
        { 107, "rwm_australia" },
        { 108, "rwm_indochina" },
        { 109, "rwm_indonesia" },
        { 110, "rwm_strait_of_malacca" },
        { 111, "rwm_phillipines" },         // Philippines : file name spelling is wrong!
        { 112, "rm_bog-islands" },
        { 113, "rm_mangrove-jungle" },
        { 114, "rm_pacific-islands" },
        { 115, "rm_sandbank" },
        { 116, "rm_water-nomad" },
        { 117, "sm_jungle-islands" },
        { 118, "sm_holy-line" },
        { 119, "sm_border-stones" },
        { 120, "sm_yin-yang" },
        { 121, "sm_jungle-lanes" },
        { 122, "rm_alpine-lakes" },
        { 123, "rm_bogland" },
        { 124, "rm_mountain-ridge" },
        { 125, "rm_ravines" },
        { 126, "rm_wolf-hill" },
        { 132, "rwm_antarctica" },
        { 133, "rwm_aral_sea" },
        { 134, "rwm_black_sea" },
        { 135, "rwm_caucasus" },
        { 136, "rwm_caucasus" },
        { 137, "Custom Map Pool" },         // Not defined!
        { 138, "Custom Map Pool" },         // Not defined!
        { 139, "rm_golden-swamp" },
        { 140, "rm_four-lakes" },
        { 141, "rm_land_nomad" },
        { 142, "br_battle_on_the_ice" },
        { 143, "br_el_dorado" },
        { 144, "br_fall_of_axum" },
        { 145, "br_fall_of_rome" },
        { 146, "br_the_majapahit_empire" },
        { 147, "rm_amazon_tunnels" },
        { 148, "rm_coastal_forest" },
        { 149, "rm_african_clearing" },
        { 150, "rm_atacama" },
        { 151, "rm_seize_the_mountain" },
        { 152, "rm_crater" },
        { 153, "rm_crossroads" },
        { 154, "rm_michi" },
        { 155, "rm_team_moats" },
        { 156, "rm_volcanic_island" },
        { 157, "rm_acclivity" },
        { 158, "rm_eruption" },
        { 159, "rm_frigid_lake" },
        { 160, "rm_greenland" },
        { 161, "rm_lowland" },
        { 162, "rm_marketplace" },
        { 163, "rm_meadow" },
        { 164, "rm_mountain_range" },
        { 165, "rm_northern_isles" },
        { 166, "rm_ring_fortress" },
        { 167, "rm_runestones" },
        { 168, "rm_aftermath" },
        { 169, "rm_enclosed" },
        { 170, "rm_haboob" },
        { 171, "rm_kawasan" },
        { 172, "rm_land_madness" },
        { 173, "rm_sacred_springs" },
        { 174, "rm_wade" },
    };

    /// <summary>
    /// Get map icon file name.
    /// </summary>
    /// <param name="mapId">Map ID.</param>
    /// <returns>File name.</returns>
    public static string GetFileName(int? mapId)
    {
        var appPath = AoE2DeApp.GetPath();

        string ret = $"{appPath}{MapiconsPath}cm_generic.DDS";

        if (mapId is int id) {
            if (MapIconFielNames.TryGetValue(id, out string fileName)) {
                ret = $"{appPath}{MapiconsPath}{fileName}.DDS";
            }
        }

        return ret;
    }
}
