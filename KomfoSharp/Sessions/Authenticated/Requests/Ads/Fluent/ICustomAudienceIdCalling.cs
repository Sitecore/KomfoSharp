// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudienceIdCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent
{
  /// <summary>
  /// Defines the call to specify the custom audience ID.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface ICustomAudienceIdCalling<out T>
  {
    /// <summary>
    /// Specifies the custom audience ID.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The result of the call.</returns>
    T CustomAudienceId(string customAudienceId);
  }
}