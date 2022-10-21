using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.Sound;

namespace MorrowRim_TelvanniSpiders
{
    public class HediffComp_SpiderSpawnerOnDeath : HediffComp
    {
        public HediffCompProperties_SpiderSpawnerOnDeath Props
        {
            get
            {
                return (HediffCompProperties_SpiderSpawnerOnDeath)this.props;
            }
        }

        public override void Notify_PawnDied()
        {
            base.Notify_PawnDied();

            SoundDef sound = Props.spawnSoundDef;
            Corpse corpse = parent.pawn.Corpse;
            sound.PlayOneShot(new TargetInfo(corpse.Position, corpse.Map, false));

            int numToSpawn = Props.numberRange.RandomInRange;
            List<Pawn> spiders = new List<Pawn> { };
            for (int i = 1; i <= numToSpawn; i++)
            {
                Pawn spider = PawnGenerator.GeneratePawn(Props.kindDef);
                PawnUtility.TrySpawnHatchedOrBornPawn(spider, corpse);
                spiders.Add(spider);
            }
            Messages.Message("MorrowRim_SpiderSpawner_Spawned".Translate(numToSpawn, Props.kindDef.label), spiders, MessageTypeDefOf.NeutralEvent, false);
        }
    }
}