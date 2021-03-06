//------------------------------------------------------------------------------
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

    [MetadataType(typeof(ClientMetadata))]
    public partial class Client
    {
        class ClientMetadata
        {
       
            [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "Required")]
            [Display(Name = "الاسم")]
            public string Title { get; set; }
            [Display(Name = "الصورة")]
            public string Image { get; set; }
            [Display(Name = "الرابط")]
            [Url(ErrorMessage = null, ErrorMessageResourceType = typeof(Resources.Labels), ErrorMessageResourceName = "URL")]
            public string Link { get; set; }

            [Display(Name = "تاريخ الإضافة")]
            public DateTime Date { get; set; }
        }
    }
}
