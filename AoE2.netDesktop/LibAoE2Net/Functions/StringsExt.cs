namespace LibAoE2net
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// extention of Language enum.
    /// </summary>
    public static class StringsExt
    {
        /// <summary>
        /// Gets the string of the ID in the list.
        /// </summary>
        /// <param name="stringIds">target list.</param>
        /// <param name="id">target id.</param>
        /// <returns>Found string.</returns>
        public static string GetString(this List<StringId> stringIds, int id)
        {
            return stringIds.Where(x => x.Id == id).First().String;
        }
    }
}
