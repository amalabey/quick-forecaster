using QuickForecaster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuickForecaster.Application.Exceptions;

namespace QuickForecaster.Application
{
    public static class SimulationEngine
    {
        private const int MAX_SIMULATIONS = 100;
        public static List<decimal> Simulations = new List<decimal>();

        public static void RunSimulations(IEnumerable<BacklogItem> backlogItems)
        {
            if(backlogItems == null || !backlogItems.Any())
            {
                throw new ArgumentException("Either backlog items is null or empty");
            }

            Simulations.Clear();

            for (int i = 0; i < MAX_SIMULATIONS; i++)
            {
                decimal simulatedDuration = RunSimulation(backlogItems);
                Simulations.Add(simulatedDuration);
            }
        }

        public static decimal PredictProbability(int duration)
        {
            var possibleSimulations = Simulations.Count(s => s <= duration);
            var probabilityOfCompletion = (possibleSimulations / Simulations.Count) * 100;
            return probabilityOfCompletion;
        }

        private static int RunSimulation(IEnumerable<BacklogItem> backlogItems)
        {
            int duration = 0;
            foreach (var backlogItem in backlogItems)
            {
                if(backlogItem.OptimisticEstimate == 0 || backlogItem.PessimisticEstimate == 0)
                {
                    throw new ValidationException("Both OptimisticEffort and PessimisticEffort must be > 0");
                }

                if(backlogItem.OptimisticEstimate > backlogItem.PessimisticEstimate)
                {
                    throw new ValidationException("Optimistic effort cannot be greater than the pessimistic effort");
                }

                duration += GetPredictedEffort(backlogItem.OptimisticEstimate, backlogItem.PessimisticEstimate);
            }

            return duration;
        }

        private static int GetPredictedEffort(decimal optimisticEffort, decimal pessimisticEffort)
        {
            Random rand = new Random();
            int effort = rand.Next((int)optimisticEffort, (int)pessimisticEffort);
            return effort;
        }
    }
}
