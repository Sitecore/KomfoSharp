// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudiencesResponse.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Represents the response to the custom audiences request.
  /// </summary>
  [Serializable]
  public class CustomAudiencesResponse : ICustomAudiencesResponse
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudiencesResponse"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public CustomAudiencesResponse(IEnumerable<Model.CustomAudience> data)
    {
      this.Data = data;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IEnumerable<Model.CustomAudience> Data { get; private set; }
  }
}