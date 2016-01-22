// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INewCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  /// <summary>
  /// Defines the response to the new custom audience request.
  /// </summary>
  public interface INewCustomAudienceResponse
  {
    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    NewCustomAudienceResponseData Data { get; }
  }
}