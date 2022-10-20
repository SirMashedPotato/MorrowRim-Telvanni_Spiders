using Verse;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
	[DefOf]
	public static class ThingDefOf
	{
		static ThingDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
		}

		public static ThingDef Filth_BloodInsect;
		public static ThingDef MorrowRim_AlbinoSpiderPod;
		public static ThingDef MorrowRim_AlbinoSpiderNestSpawner;
	}
}
