using SecretHitler.Game.Player;

namespace SecretHitler.Game.Victory;

/// <summary>
/// Win Cases:
/// <list type="bullet">
/// <item>Liberal &amp; true: Liberals win by policy enactment</item>
/// <item>Liberal &amp; false: Liberals win by executing Hitler</item>
/// <item>Fascist &amp; true: Fascists win by policy enactment</item>
/// <item>Fascist &amp; false: Fascists win by electing Hitler as Chancellor</item>
/// </list>
/// </summary>
public struct VictorySummary
{
    public PlayerRole WinningTeam { get; }
    public bool WinByEnactment { get; }
    
    /// <summary>
    /// Win Cases:
    /// <list type="bullet">
    /// <item>Liberal &amp; true: Liberals win by policy enactment</item>
    /// <item>Liberal &amp; false: Liberals win by executing Hitler</item>
    /// <item>Fascist &amp; true: Fascists win by policy enactment</item>
    /// <item>Fascist &amp; false: Fascists win by electing Hitler as Chancellor</item>
    /// </list>
    /// </summary>
    public VictorySummary(PlayerRole winningTeam, bool winByEnactment)
    {
        WinningTeam = winningTeam is PlayerRole.Hitler ? PlayerRole.Fascist : winningTeam;
        WinByEnactment = winByEnactment;
    }
}