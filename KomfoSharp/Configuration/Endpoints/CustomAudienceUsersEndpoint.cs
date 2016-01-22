// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceUsersEndpoint.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Xml.Serialization;

  /// <summary>
  /// Represents the "komfoSharp/services/endpoints/customAudiences/{audience_id}/users" endpoint.
  /// </summary>
  [Serializable]
  public class CustomAudienceUsersEndpoint : EndpointBase
  {
    /// <summary>
    /// Gets or sets the maximum entries per call.
    /// </summary>
    /// <value>
    /// The maximum entries per call.
    /// </value>
    [XmlAttribute("maxEntriesPerCall")]
    [Range(1, int.MaxValue)]
    public int MaxEntriesPerCall { get; set; }

    /// <summary>
    /// Defines the parameters names.
    /// </summary>
    public static class Parameters
    {
      /// <summary>
      /// The audience identifier parameter.
      /// </summary>
      public const string AudienceId = "audience_id";
    }
  }
}