// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFacebookApplicationsIdsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the call to specify the Facebook application IDs.
  /// </summary>
  public interface IFacebookApplicationsIdsCalling
  {
    /// <summary>
    /// Specifies the Facebook applications IDs.
    /// </summary>
    /// <param name="facebookApplicationsIds">The Facebook applications IDs.</param>
    /// <returns>The result of the call.</returns>
    IFacebookApplicationsIdsCalled FacebookApplicationsIds(IEnumerable<string> facebookApplicationsIds);
  }
}