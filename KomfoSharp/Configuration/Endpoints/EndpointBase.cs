// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndpointBase.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Xml.Serialization;

  /// <summary>
  /// Represents the endpoint.
  /// </summary>
  [Serializable]
  public abstract class EndpointBase
  {
    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>
    /// The path.
    /// </value>
    [XmlAttribute("path")]
    [Required]
    public string Path { get; set; }
  }
}
