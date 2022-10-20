using Verse;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
    [DefOf]
    public class PawnKindDefOf
    {
		static PawnKindDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
		}

		public static PawnKindDef MorrowRim_AlbinoSpider;
	}
}
