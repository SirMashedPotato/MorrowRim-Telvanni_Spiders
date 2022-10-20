using RimWorld;
using Verse;
using System.Collections.Generic;
using Verse.Sound;

namespace MorrowRim_TelvanniSpiders
{
    public static class AlbinoSpiderUtility
    {
        private static readonly float baseSpawnChance = 0.50f;
        private static readonly float additionalSpawnChance = 0.10f;

        private static readonly List<ResearchProjectDef> researchProjects = new List<ResearchProjectDef>
        {
            ResearchProjectDefOf.MorrowRim_SpiderAdvanced, ResearchProjectDefOf.MorrowRim_SpiderForge, ResearchProjectDefOf.MorrowRim_SpiderTraps
        };

        public static void SpawnAlbinoSpiderPod(Corpse spider)
        {
            if (CheckChance())
            {
                Thing thing = ThingMaker.MakeThing(ThingDefOf.MorrowRim_AlbinoSpiderPod);
                thing.stackCount = 1;
                GenPlace.TryPlaceThing(thing, spider.Position, spider.Map, ThingPlaceMode.Near);
                Messages.Message("MorrowRim_ObtainedAlbinoSpiderPod".Translate(), thing, MessageTypeDefOf.PositiveEvent);
            }
        }

        public static bool CheckChance()
        {
            return Rand.Chance(baseSpawnChance + GetAdditionalChances());
        }

        private static float GetAdditionalChances()
        {
            float addChance = 0f;
            ResearchManager researchManager = new ResearchManager();
            foreach (ResearchProjectDef research in researchProjects)
            {
                if (researchManager.GetProgress(research) == research.CostApparent)
                {
                    addChance += additionalSpawnChance;
                }
            }
            return addChance;
        }

        public static void PlaySpiderSound(Thing target)
        {
            SoundDef sound = SoundDefOf.MorrowRim_Impact_TelvanniSpider;
            sound.PlayOneShot(new TargetInfo(target.Position, target.Map, false));
        }
    }
}