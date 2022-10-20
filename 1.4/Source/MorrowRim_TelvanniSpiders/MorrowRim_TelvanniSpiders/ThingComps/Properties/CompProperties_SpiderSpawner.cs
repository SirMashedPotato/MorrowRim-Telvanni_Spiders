using Verse;

namespace MorrowRim_TelvanniSpiders
{
    public class CompProperties_SpiderSpawner : CompProperties
	{
		public CompProperties_SpiderSpawner()
		{
			this.compClass = typeof(Comp_SpiderSpawner);
		}
		public int daysPerBatch = 10;
		public int batchesToSpawn = 1;
		public int numPerBatch = 5;
		public PawnKindDef kindDef;
		public SoundDef spawnSoundDef;
	}
}