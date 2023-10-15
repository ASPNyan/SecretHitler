namespace SecretHitler.Game.Boards;

public enum PolicyAction : sbyte
{
    None,
    PolicyPeek,
    InvestigateLoyalty,
    CallSpecialElection,
    Execute,
    Veto,
}

public static class PolicyActionExtensions
{
    public static (string name, string description) GetPolicyDisplayData(this PolicyAction action)
    {
        string name = string.Empty;
        string description = string.Empty;
        
        switch (action)
        {
            case PolicyAction.PolicyPeek:
                name = "Policy Peek";
                description = "The President examines the top three cards.";
                break;
            case PolicyAction.InvestigateLoyalty:
                name = "Investigate Party Loyalty";
                description = "The President examines a Player's party membership card.";
                break;
            case PolicyAction.CallSpecialElection:
                name = "Special Presidential Election";
                description = "The President picks the next Presidential Candidate.";
                break;
            case PolicyAction.Execute:
                name = "Execution";
                description = "The President must execute a Player.";
                break;
            case PolicyAction.Veto:
                name = "Veto Power";
                description = "Veto Power is unlocked. " +
                              "Legislators may both agree to discard all tiles and continue play as normal.";
                break;
            default:
                return (name, description);
        }

        return (name, description);
    }
}