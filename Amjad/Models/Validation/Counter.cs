﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Amjad.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(CounterMetadata))]
    public partial class Counter
    {
        class CounterMetadata
        {
       
            [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
            [Display(Name = "عدد القضايا")]
            public Nullable<int> CasesCount { get; set; }
            [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
            [Display(Name = "عدد العملاء")]
            public Nullable<int> ClientsCount { get; set; }
            [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
            [Display(Name = "عدد الجوائز")]
            public Nullable<int> AwardsCount { get; set; }
            [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
            [Display(Name = "عدد المحاميين")]
            public Nullable<int> LawyersCount { get; set; }

        }
    }
}