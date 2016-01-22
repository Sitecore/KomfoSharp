// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response to the custom audience request.
  /// </summary>
  [Serializable]
  public class CustomAudienceResponse : ICustomAudienceResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CustomAudienceResponse(CustomAudience data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public CustomAudience Data { get; private set; }
  }
}