// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICustomAudiencesCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent
{
  /// <summary>
  /// Defines the call to custom audiences requests.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface ICustomAudiencesCalling<out T>
  {
    /// <summary>
    /// Gets the custom audiences requests builder.
    /// </summary>
    /// <value>
    /// The custom audiences requests builder.
    /// </value>
    T CustomAudiences { get; }
  }
}