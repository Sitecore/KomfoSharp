// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreamEndpoint.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Xml.Serialization;

  /// <summary>
  /// Represents the "stream" endpoint.
  /// </summary>
  [Serializable]
  public class StreamEndpoint : EndpointBase
  {
    /// <summary>
    /// Gets or sets the maximum twitter handles per call.
    /// </summary>
    /// <value>
    /// The maximum twitter handles per call.
    /// </value>
    [XmlAttribute("maxTwitterHandlesPerCall")]
    [Range(1, int.MaxValue)]
    public int MaxTwitterHandlesPerCall { get; set; }

    /// <summary>
    /// Gets or sets the maximum results per call.
    /// </summary>
    /// <value>
    /// The maximum results per call.
    /// </value>
    [XmlAttribute("maxResultsPerCall")]
    [Range(1, int.MaxValue)]
    public int MaxResultsPerCall { get; set; }
  }
}
