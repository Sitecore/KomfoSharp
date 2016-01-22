// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticatedRequestsBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Twitter.Fluent;

  /// <summary>
  /// Defines the methods to build authenticated requests.
  /// </summary>
  public interface IAuthenticatedRequestsBuilder : IAdsCalling, ITwitterCalling
  {
  }
}