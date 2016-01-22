// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddUsersToCustomAudienceRequestBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add
{
  using KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Add.Fluent;
  using KomfoSharp.Sessions.Authenticated.Requests.Fluent;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the add users to custom audience request builder.
  /// </summary>
  public interface IAddUsersToCustomAudienceRequestBuilder : IWithPollingCalling<IWithPollingCalled>, ICreateCalling<IAddUsersToCustomAudienceRequest>
  {
  }
}