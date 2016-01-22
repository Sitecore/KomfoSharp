// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceStatusRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the custom audience status request builder.
  /// </summary>
  public interface ICustomAudienceStatusRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<ICustomAudienceStatusRequest>
  {
  }
}