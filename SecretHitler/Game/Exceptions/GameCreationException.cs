namespace SecretHitler.Game.Exceptions;

public class GameCreationException(string parameter, string message) 
    : ArgumentException("Failed to create a game with the provided settings, due to an error with " +
                       $"the `{parameter}` parameter.\n\nError Message: " + message, parameter);