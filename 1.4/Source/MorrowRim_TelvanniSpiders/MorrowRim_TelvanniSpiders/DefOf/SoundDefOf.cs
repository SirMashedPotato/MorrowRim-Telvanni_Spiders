using Verse;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
	[DefOf]
    public class SoundDefOf
    {
		static SoundDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SoundDefOf));
		}

		public static SoundDef MorrowRim_Impact_TelvanniSpider;
	}
}
