using Verse;
using RimWorld;

namespace MorrowRim_TelvanniSpiders
{
    public class DeathActionWorker_AlbinoSpider : DeathActionWorker
    {
        public override void PawnDied(Corpse corpse)
        {
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, ThingDefOf.Filth_BloodInsect, Rand.RangeInclusive(1, 3));

            AlbinoSpiderUtility.SpawnAlbinoSpiderPod(corpse);
            AlbinoSpiderUtility.PlaySpiderSound(corpse);

            corpse.Destroy();
        }
    }
}