// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the custom audiences request builder.
  /// </summary>
  public interface ICustomAudiencesRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<ICustomAudiencesRequest>
  {
  }
}