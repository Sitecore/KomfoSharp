// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceStatusResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience.Status
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response to the custom audience status request.
  /// </summary>
  [Serializable]
  public class CustomAudienceStatusResponse : ICustomAudienceStatusResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceStatusResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CustomAudienceStatusResponse(CustomAudienceStatus data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public CustomAudienceStatus Data { get; private set; }
  }
}