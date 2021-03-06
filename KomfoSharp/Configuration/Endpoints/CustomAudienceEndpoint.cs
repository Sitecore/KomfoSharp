﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAudienceEndpoint.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Endpoints
{
  using System;

  /// <summary>
  /// Represents the "komfoSharp/services/endpoints/customAudiences/{audience_id}" endpoint.
  /// </summary>
  [Serializable]
  public class CustomAudienceEndpoint : EndpointBase
  {
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