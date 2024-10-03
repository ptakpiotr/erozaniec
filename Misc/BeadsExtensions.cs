using ERozaniec.Models;

namespace ERozaniec.Misc
{
    internal static class BeadsExtensions
    {
        internal static BeadModel? GetLastCompletedBead(this IList<BeadModel> beams)
        {
            return beams.OrderBy(b => b.Order)
                .LastOrDefault(b => b.IsCompleted);
        }

        internal static BeadModel? GetFirstNotCompletedBead(this IList<BeadModel> beams)
        {
            return beams.OrderBy(b => b.Order)
                .FirstOrDefault(b => !b.IsCompleted);
        }
    }
}