// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIntervalCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  using System;

  /// <summary>
  /// Defines the call to specify the polling interval in the current request.
  /// </summary>
  public interface IIntervalCalling
  {
    /// <summary>
    /// Specifies the polling interval in the current request.
    /// </summary>
    /// <param name="interval">The interval.</param>
    /// <returns>The current request builder.</returns>
    IIntervalCalled Interval(TimeSpan interval);
  }
}