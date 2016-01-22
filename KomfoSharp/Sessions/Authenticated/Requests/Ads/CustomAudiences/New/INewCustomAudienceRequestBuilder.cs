// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the new custom audience request builder.
  /// </summary>
  public interface INewCustomAudienceRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<INewCustomAudienceRequest>
  {
  }
}