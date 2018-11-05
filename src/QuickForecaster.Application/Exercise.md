# Exercise

This exercise aims to validate the learnings from "Unit Testing Best Practices" training session.

## Problem

“Backlog Item” entity captures “optimistic” and “pessimistic” effort values. Predicted effort for a backlog item can be 
calculated by taking a random number between "optmistic" and "pessimistic" effort values. You can derive the total forecasted
duration of the project by summing up predicted (random) effort for each backlog item. You can simulate above duration prediction
"N" number of times, by picking different random effort each time.  

Afterwards, you can pass a desired project duration to the Engine and ask it to predict the probability of completing
the project within the given duration, based on the simulations. This is a simple implemention of the "Monte-Carlo"
simulation algorithm.

*If above sounds complicated, take a look at the "SimulationEngine.cs"*.

Example Use:
```
var backlogItems = new BacklogItem[]{
    new BacklogItem{Task="Home page", OptimisticEstimate=5.0, PessimisticEstimate=10.0},
    new BacklogItem{Task="Navigation", OptimisticEstimate=5.0, PessimisticEstimate=10.0},
    new BacklogItem{Task="Create client page", OptimisticEstimate=5.0, PessimisticEstimate=10.0}};

SimulationEngine.RunSimulations(backlogItems);
var numSimulations = SimulationEngine.Simulations.Count;
Console.WriteLine($"Ran: {numSimulations} simulations")

var probability = SimulationEngine.PredictProbability(new DateTime(2018,11,10));
Console.WriteLine($"If you start the project today, there is {probability}% chance of completing it by 2018/11/10");
```

**The problem is that SimulationEngine was poorly written and was not tested.**

## Challenges

**Challenge 01:**  
Validate SimultionEngine functionality using Unit Tests.  

**Challenge 02:**  
Refactor SimulationEngine so that it is testable.  

**Challenge 03:**  
Increase the test coverage to ensure all the edge cases are covered.
