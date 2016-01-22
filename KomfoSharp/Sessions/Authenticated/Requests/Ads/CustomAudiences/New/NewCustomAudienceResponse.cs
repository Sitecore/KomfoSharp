// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewCustomAudienceResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences.New
{
  using System;

  /// <summary>
  /// Represents the response to the new custom audience request.
  /// </summary>
  [Serializable]
  public class NewCustomAudienceResponse : INewCustomAudienceResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NewCustomAudienceResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public NewCustomAudienceResponse(NewCustomAudienceResponseData data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public NewCustomAudienceResponseData Data { get; private set; }
  }
}