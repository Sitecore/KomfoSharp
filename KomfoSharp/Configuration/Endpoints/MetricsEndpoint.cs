// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetricsEndpoint.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Xml.Serialization;

  /// <summary>
  /// Represents the "metrics" endpoint.
  /// </summary>
  [Serializable]
  public class MetricsEndpoint : EndpointBase
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
  }
}
