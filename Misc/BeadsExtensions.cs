using ERozaniec.Models;

namespace ERozaniec.Misc
{
    internal static class BeadsExtensions
    {
        internal static BeadModel? GetLastCompletedBead(this IList<BeadModel> beams)
        {
            BeadModel? bead = beams.OrderBy(b => b.Order)
                .LastOrDefault(b => b.IsCompleted);

            if (bead is not null)
            {
                Preferences.Set("CurrentNumber", bead.Order);
            }

            return bead;
        }

        internal static BeadModel? GetFirstNotCompletedBead(this IList<BeadModel> beams)
        {
            BeadModel? bead = beams.OrderBy(b => b.Order)
                .FirstOrDefault(b => !b.IsCompleted);

            if (bead is not null)
            {
                Preferences.Set("CurrentNumber", bead.Order);
            }

            return bead;
        }
    }
}