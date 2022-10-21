using Verse;

namespace MorrowRim_TelvanniSpiders
{
    public class HediffCompProperties_SpiderSpawnerOnDeath : HediffCompProperties
    {

        public HediffCompProperties_SpiderSpawnerOnDeath()
        {
            this.compClass = typeof(HediffComp_SpiderSpawnerOnDeath);
        }

        public IntRange numberRange;
        public PawnKindDef kindDef;
        public SoundDef spawnSoundDef;
    }
}