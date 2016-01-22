// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the custom audience request builder.
  /// </summary>
  public interface ICustomAudienceRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<ICustomAudienceRequest>
  {
  }
}