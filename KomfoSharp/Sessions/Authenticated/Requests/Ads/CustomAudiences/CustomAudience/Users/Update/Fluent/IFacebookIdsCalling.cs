// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFacebookIdsCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Users.Update.Fluent
{
  using System.Collections.Generic;

  /// <summary>
  /// Defines the call to specify the Facebook IDs.
  /// </summary>
  public interface IFacebookIdsCalling
  {
    /// <summary>
    /// Specify the Facebook IDs.
    /// </summary>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <returns>The result of the call.</returns>
    IFacebookIdsCalled FacebookIds(IEnumerable<string> facebookIds);
  }
}