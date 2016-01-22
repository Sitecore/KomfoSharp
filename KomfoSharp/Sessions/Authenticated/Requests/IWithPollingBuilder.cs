// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWithPollingBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the interfaces to build the request with polling.
  /// </summary>
  public interface IWithPollingBuilder : IIntervalCalling, ICreateCalling<PollingRequestConfiguration>
  {
     
  }
}