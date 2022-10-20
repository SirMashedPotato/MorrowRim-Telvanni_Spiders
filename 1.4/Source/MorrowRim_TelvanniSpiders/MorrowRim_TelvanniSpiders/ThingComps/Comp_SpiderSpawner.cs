using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.Sound;

namespace MorrowRim_TelvanniSpiders
{
    public class Comp_SpiderSpawner : ThingComp
    {
		public CompProperties_SpiderSpawner Props
		{
			get
			{
				return (CompProperties_SpiderSpawner)this.props;
			}
		}

        private int ticksUntil;
        private int batchesHatched;

        public override void CompTick()
        {
            base.CompTick();

            ticksUntil -= 1;
            if (ticksUntil <= 0)
            {
                SpawnSpiders();
                batchesHatched++;
                if(batchesHatched >= Props.batchesToSpawn)
                {
                    parent.Destroy();
                }
                SetTicks();
            }
        }

        public override void PostPostMake()
        {
            base.PostPostMake();
            SetTicks();
            batchesHatched = 0;
        }

        public override string CompInspectStringExtra()
        {
            return "MorrowRim_SpiderSpawner_Tooltip".Translate(Props.batchesToSpawn - batchesHatched, (ticksUntil - Find.TickManager.TicksGame).ToStringTicksToPeriod(true, false, true, true, false));
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref ticksUntil, "ticksUntil", 0);
            Scribe_Values.Look(ref batchesHatched, "batchesHatched", 0);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Prefs.DevMode)
            {
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set progress to 0%%",
                    action = delegate ()
                    {
                        SetTicks();
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set progress to 100%",
                    action = delegate ()
                    {
                        ticksUntil = 0;
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "Debug: Set batches hatched to 0",
                    action = delegate ()
                    {
                        batchesHatched = 0;
                    }
                };
            }
            yield break;
        }

        public void SetTicks()
        {
            ticksUntil = Props.daysPerBatch * 60000; //60,000 ticks per day
        }

        public void SpawnSpiders()
        {
            SoundDef sound = Props.spawnSoundDef;
            sound.PlayOneShot(new TargetInfo(parent.Position, parent.Map, false));

            int numToSpawn = Props.numPerBatch;
            List<Pawn> spiders = new List<Pawn> { };
            for (int i = 0; i <= numToSpawn; i++)
            {
                Pawn spider = PawnGenerator.GeneratePawn(Props.kindDef);
                PawnUtility.TrySpawnHatchedOrBornPawn(spider, parent);
                spiders.Add(spider);
            }
            Messages.Message("MorrowRim_SpiderSpawner_Spawned".Translate(Props.numPerBatch, Props.kindDef.label), spiders, MessageTypeDefOf.NeutralEvent, false);
        }
    }
}
