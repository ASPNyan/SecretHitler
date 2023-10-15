using SecretHitler.Game.Player;
using SecretHitler.Game.Victory;

namespace SecretHitler.Game.Boards;

/// <summary>
/// A board representing the state of the Liberal election progress.
/// </summary>
public class LiberalBoard(Action<VictorySummary> victoryTrigger, byte maxPolicies = 5)
{
    public Action<VictorySummary> VictoryTrigger { get; } = victoryTrigger;
    public byte MaxPolicies { get; } = maxPolicies;
    public byte EnactedPolicies { get; private set; }
    
    public void EnactPolicy()
    {
        EnactedPolicies++;

        if (EnactedPolicies >= MaxPolicies) 
            VictoryTrigger(new VictorySummary(PlayerRole.Liberal, true));
    }
}