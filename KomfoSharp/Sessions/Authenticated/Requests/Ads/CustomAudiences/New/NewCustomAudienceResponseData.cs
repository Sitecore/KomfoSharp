// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceResponseData.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;

  /// <summary>
  /// Represents the data in the response to the new custom audience request.
  /// </summary>
  [Serializable]
  public class NewCustomAudienceResponseData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceResponseData"/> class.
    /// </summary>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    public NewCustomAudienceResponseData(string customAudienceId)
    {
      this.CustomAudienceId = customAudienceId;
    }

    /// <summary>
    /// Gets the custom audience identifier.
    /// </summary>
    /// <value>
    /// The custom audience identifier.
    /// </value>
    public string CustomAudienceId { get; private set; }
  }
}