// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudiencesRequest.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.CustomAudiences
{
  using System;

  /// <summary>
  /// Represents the custom audiences request.
  /// </summary>
  [Serializable]
  public class CustomAudiencesRequest : ICustomAudiencesRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAudiencesRequest"/> class.
    /// </summary>
    public CustomAudiencesRequest()
    {
      this.Configuration = new CustomAudiencesRequestConfiguration();
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    public CustomAudiencesRequestConfiguration Configuration { get; private set; }
  }
}