// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWithPollingCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  using System;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the call to enable the polling.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface IWithPollingCalling<out T>
  {
    /// <summary>
    /// Enables the polling.
    /// </summary>
    /// <param name="pollingSetupFunc">The polling setup function.</param>
    /// <returns>The result of the call.</returns>
    T WithPolling(Func<IWithPollingBuilder, ICreateCalling<PollingRequestConfiguration>> pollingSetupFunc = null);
  }
}