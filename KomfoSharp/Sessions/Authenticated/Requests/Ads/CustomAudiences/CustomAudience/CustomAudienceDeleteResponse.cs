// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceDeleteResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.CustomAudience
{
  using System;
  using KomfoSharp.Model;

  /// <summary>
  /// Represents the response to the custom audience delete request.
  /// </summary>
  [Serializable]
  public class CustomAudienceDeleteResponse : ICustomAudienceDeleteResponse
  {
     /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudienceDeleteResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CustomAudienceDeleteResponse(CustomAudience data)
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