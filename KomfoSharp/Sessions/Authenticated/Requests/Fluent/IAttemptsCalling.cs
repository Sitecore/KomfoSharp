// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAttemptsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  /// <summary>
  /// Defines the call to specify attempts count in the current request.
  /// </summary>
  public interface IAttemptsCalling
  {
    /// <summary>
    /// Specifies the attempts count in the current request.
    /// </summary>
    /// <param name="attemptsCount">The attempts count.</param>
    /// <returns>The current request builder.</returns>
    IAttemptsCalled Attempts(int attemptsCount);
  }
}