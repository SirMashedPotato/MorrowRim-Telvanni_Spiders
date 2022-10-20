using Verse;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
	[DefOf]
    public class ResearchProjectDefOf
    {
		static ResearchProjectDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ResearchProjectDefOf));
		}

		public static ResearchProjectDef MorrowRim_SpiderForge;
		public static ResearchProjectDef MorrowRim_SpiderTraps;
		public static ResearchProjectDef MorrowRim_SpiderAdvanced;
	}
}
