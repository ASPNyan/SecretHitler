using SecretHitler.Game.Player;
using SecretHitler.Game.Victory;

namespace SecretHitler.Game.Boards;

public class FascistBoard(Action<VictorySummary> victoryTrigger, Action<PolicyAction[]> policyActionTrigger, 
    Action hitlerEndGameTrigger, PolicyAction[][] actionGroups, byte maxPolicies = 6)
{
    public Action<VictorySummary> VictoryTrigger { get; } = victoryTrigger;
    public Action<PolicyAction[]> PolicyActionTrigger { get; } = policyActionTrigger;
    public Action HitlerEndGameTrigger { get; } = hitlerEndGameTrigger;
    public byte MaxPolicies { get; } = maxPolicies;

    public readonly byte HitlerEndGameTriggerTile = maxPolicies > 8
        ? (byte)MathF.Round(maxPolicies * 0.666f)
        : (byte)MathF.Ceiling(maxPolicies / 2f);

    private bool _triggeredEndGame;
    
    public PolicyAction[][] PolicyActionGroups { get; } = actionGroups;

    public byte EnactedPolicies { get; private set; }

    public PolicyAction[] EnactPolicy()
    {
        EnactedPolicies++;

        if (EnactedPolicies >= MaxPolicies)
        {
            VictoryTrigger(new VictorySummary(PlayerRole.Fascist, true));
            return Array.Empty<PolicyAction>();
        }

        if (EnactedPolicies > HitlerEndGameTriggerTile && !_triggeredEndGame)
        {
            HitlerEndGameTrigger();
            _triggeredEndGame = true;
        }

        PolicyAction[] actions = PolicyActionGroups[EnactedPolicies];

        if (actions.Length is 1 && actions.First() is PolicyAction.None) return Array.Empty<PolicyAction>();

        PolicyActionTrigger(actions);

        return actions;
    }
}